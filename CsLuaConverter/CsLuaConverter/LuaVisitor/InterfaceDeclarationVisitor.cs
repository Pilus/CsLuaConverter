namespace CsLuaConverter.LuaVisitor
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.Linq;
    using CodeElementAnalysis;
    using Providers;
    using Providers.GenericsRegistry;

    public class InterfaceDeclarationVisitor : IVisitor<InterfaceDeclaration>
    {
        public void Visit(InterfaceDeclaration element, IndentedTextWriter textWriter, IProviders providers)
        {
            throw new LuaVisitorException("Cannot visit interface without attribute knowledge.");
        }

        public static void Visit(Tuple<InterfaceDeclaration, AttributeList[], string, string[]>[] elements, IndentedTextWriter textWriter, IProviders providers)
        {
            var element = elements.Single().Item1;

            providers.TypeProvider.SetNamespaces(elements.Single().Item3, elements.Single().Item4);

            var originalScope = providers.NameProvider.CloneScope();

            RegisterGenerics(element, textWriter, providers);

            var typeObject = providers.TypeProvider.LookupType(element.Name);

            textWriter.WriteLine("[{0}] = function(interactionElement, generics, staticValues)", GetNumOfGenerics(element));
            textWriter.Indent++;

            WriteImplements(element, textWriter, providers);

            textWriter.WriteLine(
                "local typeObject = System.Type('{0}','{1}', baseTypeObject, {2}, generics, implements, interactionElement, 'Interface');",
                typeObject.Name, typeObject.Namespace, GetNumOfGenerics(element));

            WriteMembers(element, textWriter, providers);

            textWriter.WriteLine("return 'Interface', typeObject, memberProvider, nil, nil;");

            textWriter.Indent--;
            textWriter.WriteLine("end,");

            providers.NameProvider.SetScope(originalScope);
            providers.GenericsRegistry.ClearScope(GenericScope.Class);
        }

        public static int GetNumOfGenerics(InterfaceDeclaration element)
        {
            return element.Generics?.ContainedElements.Count ?? 0;
        }

        private static void RegisterGenerics(InterfaceDeclaration element, IndentedTextWriter textWriter, IProviders providers)
        {
            if (element.Generics == null)
            {
                return;
            }

            providers.GenericsRegistry.SetGenerics(element.Generics.ContainedElements.OfType<TypeParameter>().Select(e => e.Name), GenericScope.Class);
        }

        private static void WriteImplements(InterfaceDeclaration element, IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.Write("local implements = {");

            if (element.BaseList != null)
            {
                var first = true;
                foreach (var baseElement in element.BaseList.ContainedElements)
                {
                    if (!first)
                    {
                        textWriter.Write(",");
                    }
                    else
                    {
                        first = false;
                    }

                    TypeOfExpressionVisitor.WriteTypeReference(baseElement, textWriter, providers);
                }
            }

            textWriter.WriteLine("};");
        }

        private static void WriteMembers(InterfaceDeclaration element, IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.WriteLine("local memberProvider = function()");
            textWriter.Indent++;
            textWriter.WriteLine("local members = {};");

            foreach (var member in element.Elements)
            {
                VisitorList.Visit(member);
            }

            textWriter.WriteLine("return members;");
            textWriter.Indent--;
            textWriter.WriteLine("end");
        }
    }
}