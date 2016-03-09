namespace CsLuaConverter.TypeKnowledgeVisitor
{
    using System.Collections.Generic;
    using CodeElementAnalysis;
    using Providers;

    public class TypeKnowledgeDocumentVisitor : IVisitor<IEnumerable<DocumentElement>>
    {
        public void Visit(IEnumerable<DocumentElement> elements, IProviders providers)
        {
            foreach (var element in elements)
            {
                VisitorList.Visit(element, providers);
            }
        }
    }
}