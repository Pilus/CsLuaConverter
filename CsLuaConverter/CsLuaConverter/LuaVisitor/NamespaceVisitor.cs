namespace CsLuaConverter.LuaVisitor
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.Linq;
    using CodeElementAnalysis;
    using Providers;

    public class NamespaceVisitor : IVisitor<Namespace> //, IVisitor<NamespaceElement>
    {
        public void Visit(Namespace element, IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.WriteLine("{0} = {{", element.Name);
            textWriter.Indent++;
            textWriter.WriteLine("__metaType = _M.MetaTypes.NameSpace,");

            foreach (var enumNamespaceElement in element.Elements.Where(e => (e.Element is EnumDeclaration)))
            {
                providers.TypeProvider.SetNamespaces(enumNamespaceElement.NamespaceLocation, CollectUsings(enumNamespaceElement.Usings));
                VisitorList.Visit(enumNamespaceElement.Element);
            }

            var pairs = element.Elements.Where(e => !(e.Element is EnumDeclaration)).GroupBy(e => GetName(e.Element));

            foreach (var pair in pairs)
            {
                WriteNamespaceElements(pair, textWriter, providers);
            }

            element.SubNamespaces.ForEach(VisitorList.Visit);

            textWriter.Indent--;
            textWriter.WriteLine("}" + (element.IsRoot ? "" : ","));

            WriteFooter(element, textWriter, providers);
        }

        private static void WriteNamespaceElements(IGrouping<string, NamespaceElement> pair, IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.Write("{0} = _M.NE({{", pair.Key);

            var partialPairs = pair.GroupBy(e => GetNumGenerics(e.Element));
            foreach (var partialPair in partialPairs)
            {
                if (partialPair.First().Element is ClassDeclaration)
                {
                    var elements = partialPair.Select(
                            e => new Tuple<ClassDeclaration, AttributeList[], string, string[]>(
                                e.Element as ClassDeclaration,
                                e.Attributes.ToArray(),
                                e.NamespaceLocation,
                                CollectUsings(e.Usings))).ToArray();
                    ClassVisitor.Visit(elements, textWriter, providers);
                }
                else if (partialPair.First().Element is InterfaceDeclaration)
                {
                    var elements = partialPair.Select(
                            e => new Tuple<InterfaceDeclaration, AttributeList[], string, string[], string>(
                                e.Element as InterfaceDeclaration,
                                e.Attributes.ToArray(),
                                e.NamespaceLocation,
                                CollectUsings(e.Usings),
                                e.Document)).ToArray();
                    InterfaceDeclarationVisitor.Visit(elements, textWriter, providers);
                }
                else
                {
                    throw new LuaVisitorException("Unexpected namespace element " + partialPair.First().Element.GetType().Name);
                }
            }

            textWriter.WriteLine("}),");
        }

        public static string[] CollectUsings(List<UsingDirective> usings)
        {
            return usings.Select(GetFullNameOfUsing).ToArray();
        }

        public static string GetFullNameOfUsing(UsingDirective theUsing)
        {
            return string.Join(".", theUsing.Name.Names);
        }

        public static void WriteFooter(Namespace element, IndentedTextWriter textWriter, IProviders providers)
        {
            element.Elements.ForEach(e => WriteFooter(e, textWriter, providers));

            element.SubNamespaces.ForEach(e => WriteFooter(e, textWriter, providers));
        }

        public static void WriteFooter(NamespaceElement element, IndentedTextWriter textWriter, IProviders providers)
        {
            if (element.Element is ClassDeclaration)
            {
                ClassVisitor.WriteFooter((ClassDeclaration) element.Element, textWriter, providers, element.Attributes);
            }
        }

        private static string GetName(BaseElement element)
        {
            if (element is ClassDeclaration)
            {
                return ((ClassDeclaration) element).Name;
            }

            if (element is InterfaceDeclaration)
            {
                return ((InterfaceDeclaration)element).Name;
            }

            throw new LuaVisitorException("Cannot get name of object " + element.GetType().Name);
        }

        private static int GetNumGenerics(BaseElement element)
        {
            if (element is ClassDeclaration)
            {
                return ClassVisitor.GetNumOfGenerics((ClassDeclaration)element);
            }

            if (element is InterfaceDeclaration)
            {
                return InterfaceDeclarationVisitor.GetNumOfGenerics((InterfaceDeclaration)element);
            }

            throw new LuaVisitorException("Cannot get num of generic of object " + element.GetType().Name);
        }
    }

    internal struct NamespaceElementWithAttributes
    {
        public NamespaceElement Element;
        public AttributeList[] Attributes;
    }
}