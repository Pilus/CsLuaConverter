namespace CsLuaConverter.LuaVisitor
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using CodeElementAnalysis;

    public class NamespaceExtractor
    {
        public IEnumerable<Namespace> Extract(IEnumerable<DocumentElement> documents)
        {
            var list = this.ExtractElements(documents);
            return this.GenerateNamespaces(list);
        }

        private IEnumerable<Namespace> GenerateNamespaces(IEnumerable<NamespaceElement> elements)
        {
            var list = new List<Namespace>();

            foreach (var element in elements)
            {
                var ns = list.FirstOrDefault(n => n.Fits(element));

                if (ns == null)
                {
                    ns = new Namespace(element.NamespaceLocation.Split('.').First(), null);
                    list.Add(ns);
                }

                ns.Add(element);
            }

            return list;
        }

        private IEnumerable<NamespaceElement> ExtractElements(IEnumerable<DocumentElement> documents)
        {
            return documents.SelectMany(this.ExtractElements);
        }

        private IEnumerable<NamespaceElement> ExtractElements(DocumentElement document)
        {
            var list = new List<NamespaceElement>();

            var outerUsings = document.ContainedElements.Where(e => e is UsingDirective) as IEnumerable<UsingDirective>;

            var namespaces = document.ContainedElements.Where(e => e is NamespaceDeclaration);

            foreach (var baseElement in namespaces)
            {
                var ns = (NamespaceDeclaration) baseElement;
                var elements = ns.ContainedElements.Where(e => !(e is UsingDirective));
                var innerUsings = ns.ContainedElements.Where(e => e is UsingDirective) as IEnumerable<UsingDirective>;

                list.AddRange(elements.Select(element => this.GenerateNamespaceElement(string.Join(".", ns.FullName), element, outerUsings, innerUsings)));
            }

            return list;
        }

        private NamespaceElement GenerateNamespaceElement(string namespaceName, BaseElement element, IEnumerable<UsingDirective> outerUsings, IEnumerable<UsingDirective> innerUsings)
        {
            var usings = new List<UsingDirective>();
            if (outerUsings != null)
            {
                usings.AddRange(outerUsings);
            }

            if (innerUsings != null)
            {
                usings.AddRange(innerUsings);
            }

            return new NamespaceElement()
            {
                Element = element,
                NamespaceLocation = namespaceName,
                Usings = usings,
            };
        }
    }
}