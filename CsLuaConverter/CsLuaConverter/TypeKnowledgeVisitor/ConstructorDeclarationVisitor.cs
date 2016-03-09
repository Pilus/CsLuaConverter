namespace CsLuaConverter.TypeKnowledgeVisitor
{
    using CodeElementAnalysis;
    using Providers;

    public class ConstructorDeclarationVisitor : IVisitor<ConstructorDeclaration>
    {
        public void Visit(ConstructorDeclaration element, IProviders providers)
        {
            VisitorList.Visit(element.Parameters);
            VisitorList.Visit(element.Block);
        }
    }
}