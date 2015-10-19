namespace CsLuaConverter.SyntaxAnalysis
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.Linq;
    using ClassElements;
    using CsLuaConverter.Providers;
    using CsLuaConverter.Providers.TypeProvider;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;
    using Providers.GenericsRegistry;

    internal class Class : ILuaElement
    {
        private readonly List<Attribute> attributes;
        private readonly List<BaseList> baseLists = new List<BaseList>();
        private readonly List<Constructor> constructors = new List<Constructor>();
        private readonly List<Method> methods = new List<Method>();
        private readonly List<Property> properties = new List<Property>();
        private readonly List<ClassVariable> variables = new List<ClassVariable>();
        public bool IsStatic;
        private GenericsDefinition generics;
        private string name;

        public Class(List<Attribute> attributes)
        {
            this.attributes = attributes;
        }

        public void WriteLua(IndentedTextWriter textWriter, IProviders providers)
        {
            bool inheritsOtherClass = this.baseLists.Count > 0 && !this.baseLists[0].IsInterface(providers);

            List<ScopeElement> originalScope = providers.NameProvider.CloneScope();
            this.variables.ForEach(v => providers.NameProvider.AddToScope(v.GetScopeElement()));
            this.methods.ForEach(v => providers.NameProvider.AddToScope(v.GetScopeElement()));
            this.properties.ForEach(v => providers.NameProvider.AddToScope(v.GetScopeElement()));

            var type = providers.TypeProvider.LookupType(this.name);
            string fullName = type.ToString();

            var numberOfGenerics = 0;
            if (this.generics != null)
            {
                numberOfGenerics = this.generics.Names.Count;
                providers.GenericsRegistry.SetGenerics(this.generics.Names, GenericScope.Class);
            }

            providers.NameProvider.AddAllInheritedMembersToScope(this.name);

            /*
            var elements = new List<ILuaElement>
            {
                new Interfaces(this.baseLists.Skip(inheritsOtherClass ? 1 : 0).ToList(), this.generics),
                new Variables(this.IsStatic, this.variables),
                new Properties(this.IsStatic, this.properties),
                new Methods(this.IsStatic, this.methods, this.name),
                new Serializability(this.IsStatic, this.IsSerializable(), this.properties, this.variables, this.name),
                new Constructors(this.IsStatic, this.IsSerializable(), this.constructors, this.name),
            }; */

            var typeObject = type.GetTypeObject();

            textWriter.WriteLine("{0} = _M.NE({{[{1}] = function(interactionElement, generics, staticValues)", this.name, numberOfGenerics);
            textWriter.Indent++;
            // Expected: typeObject, statics, nonStatics, constructors, defaultValueProvider 
            WriteGenericsMapping(this.generics, textWriter, providers);

            textWriter.Write("local baseTypeObject, members, baseConstructors, baseElementGenerator = ");
            textWriter.Write("{0}.{1}", typeObject.BaseType.Namespace, typeObject.BaseType.Name);
            if (inheritsOtherClass && this.baseLists[0].Name.Generics != null)
            {
                textWriter.Write("[{");
                this.baseLists[0].Name.Generics.WriteLua(textWriter, providers);
                textWriter.Write("}]");
            }
            textWriter.WriteLine(".__meta(staticValues);");
            textWriter.WriteLine(
                "local typeObject = System.Type('{0}','{1}', baseTypeObject, {2}, generics, implements, interactionElement);",
                typeObject.Name, typeObject.Namespace, numberOfGenerics);

            textWriter.WriteLine("local elementGenerator = function()");
            textWriter.Indent++;

            textWriter.WriteLine("local element = baseElementGenerator();");
            textWriter.WriteLine("element.type = typeObject;");
            textWriter.Write("element[typeObject.Level] = ");
            LuaFormatter.WriteDictionary(textWriter, new Dictionary<string, object>()
            {
                // TODO: Write default values for variables here.
            });
            
            textWriter.WriteLine("return element;");
            textWriter.Indent--;

            textWriter.WriteLine("end");

            // Members
            MemberWriter(this.methods, textWriter, providers);
            MemberWriter(this.properties, textWriter, providers);

            // Constructors
            textWriter.WriteLine("local constructors = {");
            textWriter.Indent++;
            if (this.constructors.Count == 0)
            {
                LuaFormatter.WriteDictionary(textWriter, new Dictionary<string, object>()
                {
                    { "types", new Action(() => textWriter.Write("{}")) },
                    { "func", new Action(() => textWriter.Write("function(element) _M.AM(baseConstructors, {}).func(element); end")) },
                });
            }
            else
            {
                foreach (var constructor in this.constructors)
                {
                    LuaFormatter.WriteDictionary(textWriter, new Dictionary<string, object>()
                    {
                        { "types", new Action(() => textWriter.Write("{}")) },
                        { "func", new Action(() => constructor.WriteLua(textWriter, providers)) }, // TODO: Call base constructor
                    });
                    textWriter.Write(",");
                }
            }
            textWriter.WriteLine("");
            textWriter.Indent--;
            textWriter.WriteLine("};");

            textWriter.WriteLine("return 'Class', typeObject, members, constructors, elementGenerator;");
            
            textWriter.Indent--;
            textWriter.WriteLine("end}),");

            providers.GenericsRegistry.ClearScope(GenericScope.Class);
            providers.NameProvider.SetScope(originalScope);
        }

        private static void WriteGenericsMapping(GenericsDefinition generics, IndentedTextWriter textWriter, IProviders providers)
        {
            var mapping = new Dictionary<string, object>();
            textWriter.Write("local genericsMapping = ");

            if (generics != null)
            {
                for (var i = 0; i < generics.Names.Count; i++)
                {
                    var name = generics.Names[i];
                    mapping.Add(name, i + 1);
                }
                LuaFormatter.WriteDictionary(textWriter, mapping, ";", string.Empty);
            }
            else
            {
                textWriter.WriteLine("{};");
            }
        }

        private static void MemberWriter<T>(List<T> list, IndentedTextWriter textWriter, IProviders providers) where T : IClassMember
        {
            foreach (var item in list)
            {
                textWriter.Write("_M.IM(members,'{0}',", item.Name);
                var values = new Dictionary<string, object>();
                values["level"] = new Action(() => { textWriter.Write("typeObject.Level"); });
                values["memberType"] = item.MemberType;
                item.AddValues(values, textWriter, providers);
                LuaFormatter.WriteDictionary(textWriter, values);
                textWriter.WriteLine(");");
            }
        }

        public SyntaxToken Analyze(SyntaxToken token)
        {
            while (token.Parent is ClassDeclarationSyntax && token.Text != "{")
            {
                if (token.IsKind(SyntaxKind.IdentifierToken)) this.name = token.Text;
                if (token.IsKind(SyntaxKind.StaticKeyword)) this.IsStatic = true;
                token = token.GetNextToken();
            }

            if (token.Parent is TypeParameterListSyntax) // <
            {
                this.generics = new GenericsDefinition();
                token = this.generics.Analyze(token);
            }

            while (!(token.Parent is ClassDeclarationSyntax))
            {
                if (token.Parent is BaseListSyntax)
                {
                    token = this.AnalyseBaseList(token);
                }

                if (!(token.Parent is ClassDeclarationSyntax))
                {
                    token = token.GetNextToken();
                }
            }

            if (this.name == null) throw new Exception("Classname not found.");

            return this.AnalyzeContent(token);
        }

        private bool IsSerializable()
        {
            return
                this.attributes.Any(
                    a => a.attributeText.Equals("Serializable") || a.attributeText.Equals("System.Serializable"));
        }

        public void WriteMainCallLua(IndentedTextWriter writer, string nameSpace)
        {
            foreach (Method m in this.methods.Where(m => m.Name.Equals("Main") && m.Static))
            {
                writer.WriteLine("({0}.{1}()%_M.DOT).Main(nil);", nameSpace, this.name);
            }

            if (this.attributes.Any(att => att.attributeText.StartsWith("CsLuaAddOn(")))
            {
                writer.WriteLine("({0}.{1}()%_M.DOT).Execute();", nameSpace, this.name);
            }
        }

        private SyntaxToken AnalyzeContent(SyntaxToken token)
        {
            while (!(token.Parent is ClassDeclarationSyntax && token.Text == "}"))
            {
                switch (token.Parent.GetType().Name)
                {
                    case "FieldDeclarationSyntax":
                        token = this.AnalyzeFieldDeclaration(token);
                        break;
                    case "PropertyDeclarationSyntax":
                        token = this.AnalyzePropertyDeclaration(token);
                        break;
                    case "MethodDeclarationSyntax":
                        token = this.AnalyzeMethodDeclaration(token);
                        break;
                    case "ConstructorDeclarationSyntax":
                        token = this.AnalyzeConstructorDeclaration(token);
                        break;
                    case "BaseListSyntax":
                        token = this.AnalyseBaseList(token);
                        break;
                    case "ClassDeclarationSyntax":
                        break;
                    case "IndexerDeclarationSyntax":
                        break;
                    case "AttributeListSyntax":
                        token = SkipAttributeListSyntax(token);
                        break;
                    default:
                        throw new Exception(string.Format("Unexpeted token in class: {0}.", token.Parent.GetType().Name));
                }
                token = token.GetNextToken();
            }
            return token;
        }

        private static SyntaxToken SkipAttributeListSyntax(SyntaxToken token)
        {
            while (!(token.Parent is AttributeListSyntax) || token.Text != "]")
            {
                token = token.GetNextToken();
            }
            return token;
        }

        private SyntaxToken AnalyzeFieldDeclaration(SyntaxToken token)
        {
            var variable = new ClassVariable(this.name);
            this.variables.Add(variable);
            return variable.Analyze(token);
        }

        private SyntaxToken AnalyzePropertyDeclaration(SyntaxToken token)
        {
            var property = new Property(this.name);
            this.properties.Add(property);
            return property.Analyze(token);
        }

        private SyntaxToken AnalyzeMethodDeclaration(SyntaxToken token)
        {
            var method = new Method();
            this.methods.Add(method);
            return method.Analyze(token);
        }

        private SyntaxToken AnalyzeConstructorDeclaration(SyntaxToken token)
        {
            var cstor = new Constructor();
            this.constructors.Add(cstor);
            return cstor.Analyze(token);
        }

        private SyntaxToken AnalyseBaseList(SyntaxToken token)
        {
            var baseList = new BaseList();
            token = baseList.Analyze(token);
            this.baseLists.Add(baseList);
            return token;
        }
    }
}