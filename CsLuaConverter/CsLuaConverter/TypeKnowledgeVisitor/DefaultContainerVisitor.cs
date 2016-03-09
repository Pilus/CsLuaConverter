namespace CsLuaConverter.TypeKnowledgeVisitor
{
    using CodeElementAnalysis;
    using Providers;

    public class DefaultContainerVisitor : IVisitor<ContainerElement>
    {
        public void Visit(ContainerElement element, IProviders providers)
        {
            foreach (var e in element.ContainedElements)
            {
                VisitorList.Visit(e);
            }
        }
    }
}