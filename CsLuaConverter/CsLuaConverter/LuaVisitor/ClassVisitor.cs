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

            textWriter.WriteLine("{0} = _M.NE({{[{1}] = function(interactionElement, generics, staticValues)", element.Name, GetNumOfGenerics(element));
            textWriter.Indent++;

            WriteGenericsMapping(element, textWriter, providers);
            WriteBaseInheritance(element, textWriter, providers);
            WriteTypeGeneration(element, textWriter, providers);
            WriteElementGeneratorFunction(element, textWriter, providers);
            WriteStaticValues(element, textWriter, providers);
            WriteInitialize(element, textWriter, providers);

            textWriter.Indent--;
            textWriter.WriteLine("end}),");

            providers.GenericsRegistry.ClearScope(GenericScope.Class);
            providers.NameProvider.SetScope(originalScope);
        }

        private static void WriteBaseInheritance(ClassDeclaration element, IndentedTextWriter textWriter, IProviders providers)
        {
            var firstBaseElement = element.BaseList?.ContainedElements.First();
            var baseTypeNamespace = "System";
            var baseTypeName = "Object";

            IdentifierName inhetitedClass = null;

            if (firstBaseElement != null)
            {
                var identifierName = (IdentifierName)firstBaseElement;
                var type = identifierName.GetTypeObject(providers);

                if (type.IsClass)
                {
                    inhetitedClass = identifierName;
                    baseTypeNamespace = type.Namespace;
                    baseTypeName = type.Name;
                }
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

            // TODO: Write default values for non static variable values.

            var properties = element.ContainedElements.Where(e => e is FieldDeclaration);
            foreach (var prop in properties)
            {
                PropertyVisitor.WriteDefaultValue(prop as FieldDeclaration, textWriter, providers, false);
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

            // TODO: Write default values for static variable values.

            var properties = element.ContainedElements.Where(e => e is FieldDeclaration);
            foreach (var prop in properties)
            {
                PropertyVisitor.WriteDefaultValue(prop as FieldDeclaration, textWriter, providers, true);
            }

            textWriter.Indent--;

            textWriter.WriteLine("};");
        }

        private static void WriteInitialize(ClassDeclaration element, IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.WriteLine("local initialize = function(element, values)");
            textWriter.Indent++;

            textWriter.WriteLine("if baseInitialize then baseInitialize(element, values); end");

            // TODO: Write variables

            
            var properties = element.ContainedElements.Where(e => e is FieldDeclaration);
            foreach (var property in properties)
            {
                PropertyVisitor.WriteInitialValue(property as FieldDeclaration, textWriter, providers, false);
            }
            textWriter.Indent--;

            textWriter.WriteLine("end");
        }

        private static int GetNumOfGenerics(ClassDeclaration element)
        {
            return element.Generics?.ContainedElements.Count ?? 0;
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
    }
}