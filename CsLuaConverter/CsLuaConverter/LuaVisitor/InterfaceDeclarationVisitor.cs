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
            var attributes = elements.SelectMany(e => e.Item2).Distinct();
            var element = elements.Single().Item1;

            providers.TypeProvider.SetNamespaces(elements.Single().Item3, elements.Single().Item4);

            var originalScope = providers.NameProvider.CloneScope();

            RegisterGenerics(element, textWriter, providers);

            var typeObject = providers.TypeProvider.LookupType(element.Name);

            textWriter.WriteLine("[{0}] = function(interactionElement, generics, staticValues)", GetNumOfGenerics(element));
            textWriter.Indent++;

            WriteGenericsMapping(element, textWriter, providers);
            WriteImplements(element, textWriter, providers);

            textWriter.WriteLine(
                "local typeObject = System.Type('{0}','{1}', baseTypeObject, {2}, generics, implements, interactionElement, 'Interface');",
                typeObject.Name, typeObject.Namespace, GetNumOfGenerics(element));

            WriteAttributes(attributes, textWriter, providers);


            WriteMembers(elements.Select(e => e.Item1).ToArray(), textWriter, providers);

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

        private static void WriteMembers(InterfaceDeclaration[] element, IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.WriteLine("local memberProvider = function()");
            textWriter.Indent++;
            textWriter.WriteLine("local members = {};");

            textWriter.WriteLine("_M.GAM(members, implements);");

            foreach (var member in element.SelectMany(e => e.Elements))
            {
                VisitorList.Visit(member);
            }

            textWriter.WriteLine("return members;");
            textWriter.Indent--;
            textWriter.WriteLine("end");
        }

        private static void WriteAttributes(IEnumerable<AttributeList> attributes, IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.WriteLine("local attributes = {");
            textWriter.Indent++;

            foreach (var attribute in attributes)
            {
                VisitorList.Visit(attribute);
            }

            textWriter.Indent--;
            textWriter.WriteLine("};");
        }

        private static void WriteGenericsMapping(InterfaceDeclaration element, IndentedTextWriter textWriter, IProviders providers)
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