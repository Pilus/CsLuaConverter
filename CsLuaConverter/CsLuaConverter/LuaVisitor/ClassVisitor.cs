namespace CsLuaConverter.LuaVisitor
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection.Emit;
    using CodeElementAnalysis;
    using Microsoft.CodeAnalysis.CSharp.Syntax;
    using Providers;
    using Providers.GenericsRegistry;
    using Providers.TypeProvider;
    using SyntaxAnalysis.ClassElements;

    public class ClassVisitor : IVisitor<ClassDeclaration>
    {
        public void Visit(ClassDeclaration element, IndentedTextWriter textWriter, IProviders providers)
        {
            var originalScope = providers.NameProvider.CloneScope();
            providers.NameProvider.AddAllInheritedMembersToScope(element.Name);

            textWriter.WriteLine("{0} = _M.NE({{[{1}] = function(interactionElement, generics, staticValues)", element.Name, GetNumOfGenerics(element));
            textWriter.Indent++;

            RegisterGenerics(element, textWriter, providers);

            WriteGenericsMapping(element, textWriter, providers);
            WriteBaseInheritance(element, textWriter, providers);
            WriteTypeGeneration(element, textWriter, providers);
            WriteElementGeneratorFunction(element, textWriter, providers);
            WriteStaticValues(element, textWriter, providers);
            WriteInitialize(element, textWriter, providers);
            WriteMembers(element, textWriter, providers);
            WriteConstructors(element, textWriter, providers);

            textWriter.Write("return 'Class', typeObject, members, constructors, elementGenerator, nil, initialize;");

            textWriter.Indent--;
            textWriter.WriteLine("end}),");

            providers.GenericsRegistry.ClearScope(GenericScope.Class);
            providers.NameProvider.SetScope(originalScope);
        }

        public static void WriteFooter(ClassDeclaration element, IndentedTextWriter textWriter, IProviders providers, List<AttributeList> attributes)
        {
            if (attributes.Any(e => e.IdentifierName.Names.SingleOrDefault() == "CsLuaAddOn"))
            {
                var type = providers.TypeProvider.LookupType(element.Name);
                textWriter.Write("(");
                textWriter.Write(type.GetTypeObject().FullName);
                textWriter.WriteLine("() % _M.DOT).Execute();");
            }
        }


        private static void WriteBaseInheritance(ClassDeclaration element, IndentedTextWriter textWriter, IProviders providers)
        {
            var firstBaseElementPair = element.BaseList?.ContainedElements.First();
            var baseTypeNamespace = "System";
            var baseTypeName = "Object";

            BaseElement inhetitedClass = null;

            if (firstBaseElementPair is IdentifierName)
            {
                var identifierName = (IdentifierName)firstBaseElementPair;
                var type = identifierName.GetTypeObject(providers);

                if (type.IsClass)
                {
                    inhetitedClass = identifierName;
                    baseTypeNamespace = type.Namespace;
                    baseTypeName = type.Name;
                }
            }
            else if (firstBaseElementPair is GenericName)
            {
                var identifierName = (GenericName)firstBaseElementPair;
                var type = providers.TypeProvider.LookupType(identifierName.Name).GetTypeObject();

                if (type.IsClass)
                {
                    inhetitedClass = identifierName;
                    baseTypeNamespace = type.Namespace;
                    baseTypeName = type.Name;
                };
            }

            textWriter.Write("local baseTypeObject, members, baseConstructors, baseElementGenerator, implements, baseInitialize = ");
            textWriter.Write("{0}.{1}", baseTypeNamespace, baseTypeName);

            if (inhetitedClass != null)
            {
                // TODO: Analyse the generics of the base element call and use them to initialize the right version of the base element.
                // textWriter.Write("[{");
                // this.baseLists[0].Name.Generics.WriteLua(textWriter, providers);
                // textWriter.Write("}]");
            }

            textWriter.WriteLine(".__meta(staticValues);");
        }

        private static void WriteTypeGeneration(ClassDeclaration element, IndentedTextWriter textWriter, IProviders providers)
        {
            var typeObject = providers.TypeProvider.LookupType(element.Name).GetTypeObject();

            textWriter.WriteLine(
                "local typeObject = System.Type('{0}','{1}', baseTypeObject, {2}, generics, implements, interactionElement);",
                typeObject.Name, typeObject.Namespace, element.Generics?.ContainedElements.Count ?? 0);
        }

        private static void WriteElementGeneratorFunction(ClassDeclaration element, IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.WriteLine("local elementGenerator = function()");
            textWriter.Indent++;

            textWriter.WriteLine("local element = baseElementGenerator();");
            textWriter.WriteLine("element.type = typeObject;");
            textWriter.WriteLine("element[typeObject.Level] = {");

            textWriter.Indent++;

            // TODO: Write default values for non static property values.

            var fields = element.ContainedElements.Where(e => e is FieldDeclaration);
            foreach (var field in fields)
            {
                FieldDeclarationVisitor.WriteDefaultValue(field as FieldDeclaration, textWriter, providers, false);
            }

            textWriter.Indent--;

            textWriter.WriteLine("};");

            textWriter.WriteLine("return element;");
            textWriter.Indent--;

            textWriter.WriteLine("end");

        }

        private static void WriteStaticValues(ClassDeclaration element, IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.WriteLine("staticValues[typeObject.Level] = {");

            textWriter.Indent++;

            // TODO: Write default values for static property values.

            var fields = element.ContainedElements.Where(e => e is FieldDeclaration);
            foreach (var field in fields)
            {
                FieldDeclarationVisitor.WriteDefaultValue(field as FieldDeclaration, textWriter, providers, true);
            }

            textWriter.Indent--;

            textWriter.WriteLine("};");
        }

        private static void WriteInitialize(ClassDeclaration element, IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.WriteLine("local initialize = function(element, values)");
            textWriter.Indent++;

            textWriter.WriteLine("if baseInitialize then baseInitialize(element, values); end");

            // TODO: Write properties


            var fields = element.ContainedElements.OfType<FieldDeclaration>();
            foreach (var field in fields)
            {
                FieldDeclarationVisitor.WriteInitializeValue(field, textWriter, providers, false);
            }

            var properties = element.ContainedElements.OfType<PropertyDeclaration>();
            foreach (var property in properties)
            {
                PropertyDeclarationVisitor.WriteInitializeValue(property, textWriter, providers);
            }

            textWriter.Indent--;
            textWriter.WriteLine("end");
        }

        private static int GetNumOfGenerics(ClassDeclaration element)
        {
            return element.Generics?.ContainedElements.Count ?? 0;
        }

        private static void RegisterGenerics(ClassDeclaration element, IndentedTextWriter textWriter, IProviders providers)
        {
            if (element.Generics == null)
            {
                return;
            }

            providers.GenericsRegistry.SetGenerics(element.Generics.ContainedElements.OfType<TypeParameter>().Select(e => e.Name), GenericScope.Class);
        }

        private static void WriteGenericsMapping(ClassDeclaration element, IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.Write("local genericsMapping = ");

            if (element.Generics != null)
            {
                textWriter.Write("{");
                VisitorList.Visit(element.Generics);
                textWriter.WriteLine("};");
            }
            else
            {
                textWriter.WriteLine("{};");
            }
        }

        private static void WriteMembers(ClassDeclaration element, IndentedTextWriter textWriter, IProviders providers)
        {
            foreach (var member in element.ContainedElements.Where(m => !(m is ConstructorDeclaration)).OrderBy(GetMemberOrder))
            {
                VisitorList.Visit(member);
            }
        }

        private static int GetMemberOrder(BaseElement element)
        {
            if (element is MethodDeclaration)
            {
                return 1;
            }

            if (element is PropertyDeclaration)
            {
                return 2;
            }

            if (element is FieldDeclaration)
            {
                return 3;
            }

            throw new LuaVisitorException("Unknown member");
        }


        private static void WriteConstructors(ClassDeclaration element, IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.WriteLine("local constructors = {");
            textWriter.Indent++;

            var constuctors = element.ContainedElements.OfType<ConstructorDeclaration>().ToList();

            if (constuctors.Count == 0)
            {
                textWriter.WriteLine("{");
                textWriter.Indent++;

                textWriter.WriteLine("types = {},");
                textWriter.WriteLine("func = function(element) _M.AM(baseConstructors, {}).func(element); end,");

                textWriter.Indent--;
                textWriter.WriteLine("}");
            }
            else
            {
                foreach (var constructor in constuctors)
                {
                    textWriter.WriteLine("{");
                    textWriter.Indent++;
                    
                    var scope = providers.NameProvider.CloneScope();

                    textWriter.Write("types = {");
                    ParameterListVisitor.VisitParameterListTypeReferences(constructor.Parameters, textWriter, providers);
                    textWriter.WriteLine("},");

                    textWriter.Write("func = function(element");
                    if (constructor.Parameters != null && constructor.Parameters.ContainedElements.Count > 0)
                    {
                        textWriter.Write(",");
                        VisitorList.Visit(constructor.Parameters);
                    }

                    textWriter.WriteLine(")");

                    textWriter.Indent++;
                    if (constructor.BaseConstructorInitializer != null)
                    {
                        textWriter.Write("element.__base");
                        VisitorList.Visit(constructor.BaseConstructorInitializer.ArgumentList);
                        textWriter.WriteLine(";");
                    }
                    else
                    {
                        textWriter.WriteLine("_M.AM(baseConstructors,{}).func(element);");
                    }

                    textWriter.Indent--;

                    VisitorList.Visit(constructor.Block);
                    textWriter.WriteLine("end,");

                    providers.NameProvider.SetScope(scope);

                    textWriter.Indent--;
                    textWriter.WriteLine("},");
                }
            }

            textWriter.Indent--;
            textWriter.WriteLine("};");
        }
    }
}