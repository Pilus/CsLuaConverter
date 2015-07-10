

namespace CsLuaConverter.Providers.PartialElementRegistry
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using CsLuaConverter.SyntaxAnalysis;
    using System.Linq;

    internal class PartialElementRegistry : IPartialElementRegistry
    {
        private Dictionary<string, List<PartialElementWithUsings>> partialElements = new Dictionary<string, List<PartialElementWithUsings>>();

        private void AddElement(PartialElementWithUsings element)
        {
            if (this.partialElements.ContainsKey(element.element.Name))
            {
                partialElements[element.element.Name].Add(element);
            }
            else
            {
                partialElements[element.element.Name] = new List<PartialElementWithUsings>()
                {
                    element
                };
            }
        }

        public void Register(IPartialLuaElement element, string fullNamespaceName, List<string> usings)
        {
            this.AddElement(new PartialElementWithUsings()
            {
                element = element,
                fullNamespaceName = fullNamespaceName,
                usings = usings,
            });
        }

        private List<string> GetUsings(List<PartialElementWithUsings> elements)
        {
            var list = new List<string>();
            foreach (var element in elements)
            {
                list.AddRange(element.usings);
            }

            return list;
        }

        public void WriteLua(IndentedTextWriter textWriter, IProviders providers)
        {
            foreach (var partedElementsWithSameNamePair in this.partialElements)
            {
                var elements = partedElementsWithSameNamePair.Value;
                var firstElement = elements.First();
                var usings = GetUsings(elements);

                providers.TypeProvider.SetNamespaces(firstElement.fullNamespaceName, usings);

                elements.ForEach(e => e.element.WriteLua(textWriter, providers));
            }

            this.partialElements = new Dictionary<string, List<PartialElementWithUsings>>();
        }
    }

    internal struct PartialElementWithUsings
    {
        public IPartialLuaElement element;
        public string fullNamespaceName;
        public List<string> usings;
    }
}
