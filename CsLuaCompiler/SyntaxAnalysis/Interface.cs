namespace CsLuaCompiler.SyntaxAnalysis
{
    using System;
    using System.CodeDom.Compiler;
    using CsLuaCompiler.Providers;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp.Syntax;
    using System.Collections.Generic;
    using CsLuaCompiler.SyntaxAnalysis.ClassElements;

    internal class Interface : ILuaElement
    {
        private string name;
        private List<BaseList> baseLists = new List<BaseList>();
        private List<InterfaceMethod> methods = new List<InterfaceMethod>();
        private List<InterfaceProperty> properties = new List<InterfaceProperty>();
        private Generics generics;

        public void WriteLua(IndentedTextWriter textWriter, IProviders providers)
        {
            var originalScope = providers.NameProvider.CloneScope();
            if (this.generics != null)
            {
                this.generics.AddToScope(providers);
            }
            
            textWriter.WriteLine("{0} = function(generics)", this.name);
            textWriter.Indent++;
            textWriter.WriteLine("return {");
            textWriter.Indent++;
            textWriter.WriteLine("__isInterface = true,");

            this.WriteMethods(textWriter, providers);
            this.WriteProperties(textWriter, providers);

            textWriter.Indent--;
            textWriter.WriteLine("};");
            textWriter.Indent--;
            textWriter.WriteLine("end,");
            providers.NameProvider.SetScope(originalScope);
        }

        private void WriteMethods(IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.WriteLine("methods = {");
            textWriter.Indent++;
            foreach (var method in this.methods)
            {
                LuaFormatter.WriteDictionary(textWriter, new Dictionary<string, object>()
                {
                    { "name",  method.Name},
                    { "signature", new Action(() => {  textWriter.Write("{{{0}}}", method.Paramters.FullTypesAsString(providers)); })  },
                    { "returnType", new Action(() =>
                        {
                            textWriter.Write(DetermineTypeName(providers, method.ReturnType.Names));
                        })
                    },
                }, ",", string.Empty);
            }

            textWriter.Indent--;
            textWriter.WriteLine("},");
        }

        private static string DetermineTypeName(IProviders providers, List<string> names)
        {
            if (names.Count == 1)
            {
                var name = names[0];
                switch (name)
                {
                    case "object":
                    case "bool":
                    case "double":
                    case "int":
                    case "string":
                    case "long":
                        return "'" + name + "'";
                    case "void":
                        return "nil";
                    default:
                        break;
                }

                if (providers.GenericsRegistry.IsGeneric(name))
                {
                    return "'" + name + "'";
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

            }

            textWriter.Indent--;
            textWriter.WriteLine("},");
        }

        public SyntaxToken Analyze(SyntaxToken token)
        {
            LuaElementHelper.CheckType(typeof(InterfaceDeclarationSyntax), token.Parent);
            if (token.Text == "public" || token.Text == "private" || token.Text == "protected") // access modifier.
            {
                token = token.GetNextToken();
                LuaElementHelper.CheckType(typeof(InterfaceDeclarationSyntax), token.Parent);
            }

            if (token.Text != "interface")
            {
                throw new CompilerException("Interface expected.");
            }
            token = token.GetNextToken();

            LuaElementHelper.CheckType(typeof(InterfaceDeclarationSyntax), token.Parent);
            this.name = token.Text;
            token = token.GetNextToken();            

            if (token.Parent is TypeParameterListSyntax) // <
            {
                this.generics = new Generics();
                token = this.generics.Analyze(token);
            }

            while (token.Parent is BaseListSyntax)
            {
                var baseList = new BaseList();
                token = baseList.Analyze(token);
                this.baseLists.Add(baseList);
                token = token.GetNextToken();
            }

            if (token.Parent is TypeParameterConstraintClauseSyntax) // where (for generics)
            {
                while (!(token.Parent is InterfaceDeclarationSyntax))
                {
                    token = token.GetNextToken();
                }
            }

            LuaElementHelper.CheckType(typeof(InterfaceDeclarationSyntax), token.Parent);
            if (token.Text != "{")
            {
                throw new CompilerException("Start of interface body expected.");
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
                        Paramters = parameters,
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
                else
                {
                    throw new CompilerException(string.Format("Unexpected element in interface: '{0}'.", token.Parent.GetType().Name));
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
        public ParameterList Paramters;
    }

    internal struct InterfaceProperty
    {
        public string Name;
        public VariableName Type;
    }

}