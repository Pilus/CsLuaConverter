namespace CsLuaConverter.SyntaxAnalysis
{
    using System;
    using System.CodeDom.Compiler;
    using CsLuaConverter.Providers;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp.Syntax;
    using System.Collections.Generic;
    using CsLuaConverter.SyntaxAnalysis.ClassElements;
    using System.Linq;
    using Providers.GenericsRegistry;

    internal class Interface : IPartialLuaElement
    {
        private static Dictionary<string, Interface> firstPartials = new Dictionary<string, Interface>();

        private List<BaseList> baseLists = new List<BaseList>();
        private List<InterfaceMethod> methods = new List<InterfaceMethod>();
        private List<InterfaceProperty> properties = new List<InterfaceProperty>();
        private GenericsDefinition generics;
        private List<Attribute> attributes;
        private VariableName indexerType;

        public string Name { get; private set; }
        public bool IsPartial { get; private set; }

        public Interface(List<Attribute> attributes)
        {
            this.attributes = attributes;
        }

        private bool HasAttribute(string attributeName)
        {
            return this.attributes.Any(att => att.attributeText.Equals(attributeName) || att.attributeText.Equals(attributeName + "Attribute"));
        }

        public void WriteLua(IndentedTextWriter textWriter, IProviders providers)
        {
            if (this.IsPartial && firstPartials[this.Name] != this)
            {
                return;
            }

            var originalScope = providers.NameProvider.CloneScope();
            if (this.generics != null)
            {
                providers.GenericsRegistry.SetGenerics(this.generics.Names, GenericScope.Class);
            }
            
            textWriter.WriteLine("{0} = function(generics)", this.Name);
            textWriter.Indent++;

            this.WriteGenericsMapping(textWriter, providers);
            this.WriteImplementedInterfaces(textWriter, providers);

            textWriter.WriteLine("return {");
            textWriter.Indent++;
            textWriter.WriteLine("isInterface = true,");
            textWriter.WriteLine("name = '{0}',", Name);
            textWriter.WriteLine("implementedInterfaces = implementedInterfaces,");
            textWriter.WriteLine("ShouldProvideSelf = function()");
            textWriter.Indent++;
            if (this.HasAttribute("ProvideSelf"))
            {
                textWriter.WriteLine("return true;");
            }
            else
            {
                textWriter.WriteLine("for _, interface in ipairs(implementedInterfaces) do");
                textWriter.Indent++;
                textWriter.WriteLine("if interface.ShouldProvideSelf() then return true; end");
                textWriter.Indent--;
                textWriter.WriteLine("end");
                textWriter.WriteLine("return false;");
            }
            textWriter.Indent--;
            textWriter.WriteLine("end,");

            if (this.indexerType != null)
            {
                textWriter.WriteLine("indexer = {0},", this.indexerType.GetTypeResult(providers).ToQuotedString());
            }

            this.WriteAddImplementedSignatures(textWriter, providers);
            this.WriteMethods(textWriter, providers);
            this.WriteProperties(textWriter, providers);

            textWriter.Indent--;
            textWriter.WriteLine("};");
            textWriter.Indent--;
            textWriter.WriteLine("end,");
            providers.NameProvider.SetScope(originalScope);
            providers.GenericsRegistry.ClearScope(GenericScope.Class);
        }

        private void WriteImplementedInterfaces(IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.WriteLine("local implementedInterfaces = {");
            textWriter.Indent++;

            foreach (var baselist in this.baseLists)
            {
                var fullName = baselist.GetFullNameString(providers);
                textWriter.Write("CsLuaMeta.GetByFullName({0})(", fullName);
                if (baselist.Name.Generics != null)
                {
                    baselist.Name.Generics.WriteLua(textWriter, providers);
                }
                else
                {
                    textWriter.Write("nil");
                }
                textWriter.Write("),");
            }

            textWriter.Indent--;
            textWriter.WriteLine("};");
        }

        private void WriteAddImplementedSignatures(IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.WriteLine("__AddImplementedSignatures = function(signatures)"); 
            textWriter.Indent++;
            textWriter.WriteLine("for _, interface in ipairs(implementedInterfaces) do"); 
            textWriter.Indent++;
            textWriter.WriteLine("interface.__AddImplementedSignatures(signatures);"); 
            textWriter.Indent--;
            textWriter.WriteLine("end"); 
            textWriter.WriteLine("table.insert(signatures, {{{0}}})", providers.TypeProvider.LookupType(this.Name).ToQuotedString());
            textWriter.Indent--;
            textWriter.WriteLine("end,");
        }

        private void WriteGenericsMapping(IndentedTextWriter textWriter, IProviders providers)
        {
            var mapping = new Dictionary<string, object>();
            textWriter.Write("local genericsMapping = ");

            if (this.generics != null)
            {
                for (var i = 0; i < this.generics.Names.Count; i++)
                {
                    var name = this.generics.Names[i];
                    mapping.Add(name, i + 1);
                }
                LuaFormatter.WriteDictionary(textWriter, mapping, ";", string.Empty);
            }
            else
            {
                textWriter.WriteLine("{};");
            }            
        }

        private void WriteMethods(IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.WriteLine("methods = {");
            textWriter.Indent++;
            foreach (var method in this.methods)
            {
                if (method.ParameterList.Generics != null)
                {
                    providers.GenericsRegistry.SetGenerics(method.ParameterList.Generics.Names, GenericScope.Method);
                }
                
                LuaFormatter.WriteDictionary(textWriter, new Dictionary<string, object>()
                {
                    { "name",  method.Name},
                    { "signature", new Action(() => 
                        {
                            textWriter.Write("{");
                            foreach (var parameter in method.ParameterList.Parameters)
                            {
                                textWriter.Write("{"+ DetermineTypeName(providers, new []{ ((Parameter) parameter).Type.GetTypeString()}) + "},");
                            }
                            textWriter.Write("}");
                        })
                    },
                    { "returnType", new Action(() =>
                        {
                            textWriter.Write(DetermineTypeName(providers, method.ReturnType.Names));
                        })
                    },
                }, ",", string.Empty);

                providers.GenericsRegistry.ClearScope(GenericScope.Method);
            }

            textWriter.Indent--;
            textWriter.WriteLine("},");
        }

        private static string DetermineTypeName(IProviders providers, ICollection<string> names)
        {
            if (names.Count == 1)
            {
                var name = names.Single();
                if (providers.GenericsRegistry.IsGeneric(name))
                {
                    if (providers.GenericsRegistry.GetGenericScope(name) == GenericScope.Method)
                    {
                        // TODO: Find a way to do generic mappings for method generics in interfaces.
                        return "object";
                    }

                    return "generics[genericsMapping['" + name + "']]";
                }
            }
            return "'" + providers.TypeProvider.LookupType(names) + "'";
        }

        private void WriteProperties(IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.WriteLine("properties = {");
            textWriter.Indent++;
            foreach (var property in this.properties)
            {
                LuaFormatter.WriteDictionary(textWriter, new Dictionary<string, object>()
                {
                    { "name",  property.Name},
                    { "type", new Action(() =>
                        {
                            textWriter.Write(DetermineTypeName(providers, property.Type.Names));
                        })
                    },
                }, ",", string.Empty);
            }

            textWriter.Indent--;
            textWriter.WriteLine("},");
        }

        public SyntaxToken Analyze(SyntaxToken token)
        {
            var initialToken = token;
            LuaElementHelper.CheckType(typeof(InterfaceDeclarationSyntax), token.Parent);
            if (token.Text == "public" || token.Text == "private" || token.Text == "protected") // access modifier.
            {
                token = token.GetNextToken();
                LuaElementHelper.CheckType(typeof(InterfaceDeclarationSyntax), token.Parent);
            }

            if (token.Text == "partial")
            {
                this.IsPartial = true;
                token = token.GetNextToken();
            }

            if (token.Text != "interface")
            {
                throw new ConverterException("Interface expected.");
            }
            token = token.GetNextToken();

            LuaElementHelper.CheckType(typeof(InterfaceDeclarationSyntax), token.Parent);
            this.Name = token.Text;

            if (this.IsPartial)
            {
                if (!firstPartials.ContainsKey(this.Name))
                {
                    firstPartials.Add(this.Name, this);
                }
                else if (firstPartials[this.Name] != this)
                {
                    return firstPartials[this.Name].Analyze(initialToken);
                }
            }
            

            token = token.GetNextToken();

            if (token.Parent is TypeParameterListSyntax) // <
            {
                this.generics = new GenericsDefinition();
                token = this.generics.Analyze(token);
            }

            while (token.Parent is BaseListSyntax)
            {
                var baseList = new BaseList();
                token = baseList.Analyze(token);
                this.baseLists.Add(baseList);
                if (token.Text != "{") { 
                    token = token.GetNextToken();
                }
            }

            if (token.Parent is TypeParameterConstraintClauseSyntax) // where (for generics, when there is no inheritance)
            {
                while (!(token.Parent is InterfaceDeclarationSyntax))
                {
                    token = token.GetNextToken();
                }
            }

            LuaElementHelper.CheckType(typeof(InterfaceDeclarationSyntax), token.Parent);
            if (token.Text != "{")
            {
                throw new ConverterException("Start of interface body expected.");
            }
            token = token.GetNextToken();

            VariableName returnType = new VariableName(true);
            while (!(token.Parent is InterfaceDeclarationSyntax && token.Text == "}"))
            {
                if (token.Parent is PredefinedTypeSyntax || token.Parent is IdentifierNameSyntax || token.Parent is GenericNameSyntax)
                {
                    returnType = new VariableName(true);
                    token = returnType.Analyze(token);
                }
                else if (token.Parent is NullableTypeSyntax)
                {
                    // Skip token.
                }
                else if (token.Parent is MethodDeclarationSyntax)
                {
                    var name = token.Text;
                    token = token.GetNextToken();

                    var parameters = new ParameterList();
                    token = parameters.Analyze(token);
                    token = token.GetNextToken();

                    this.methods.Add(new InterfaceMethod()
                    {
                        Name = name,
                        ParameterList = parameters,
                        ReturnType = returnType,
                    });

                    returnType = null;
                }
                else if (token.Parent is PropertyDeclarationSyntax)
                {
                    var name = token.Text;
                    token = token.GetNextToken();

                    while (!(token.Parent is AccessorListSyntax && token.Text == "}"))
                    {
                        token = token.GetNextToken();
                    }

                    this.properties.Add(new InterfaceProperty()
                    {
                        Name = name,
                        Type = returnType,
                    });
                    returnType = null;
                }
                else if (token.Parent is AttributeListSyntax) // tags, such as Obsolete
                {
                    while (!(token.Parent is AttributeListSyntax && token.Text == "]"))
                    {
                        token = token.GetNextToken();
                    }
                }
                else if (token.Parent is IndexerDeclarationSyntax)
                {
                    token = token.GetNextToken();
                    token = token.GetNextToken();
                    this.indexerType = new VariableName(true);
                    token = this.indexerType.Analyze(token);

                    while (!(token.Parent is AccessorListSyntax && token.Text == "}"))
                    {
                        token = token.GetNextToken();
                    }
                }
                else
                {
                    throw new ConverterException(string.Format("Unexpected element in interface: '{0}'.", token.Parent.GetType().Name));
                }
                token = token.GetNextToken();
            }

            return token;
        }
    }

    internal struct InterfaceMethod
    {
        public string Name;
        public VariableName ReturnType;
        public ParameterList ParameterList;
    }

    internal struct InterfaceProperty
    {
        public string Name;
        public VariableName Type;
    }

}