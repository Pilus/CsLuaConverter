namespace CsLuaConverter.LuaVisitor
{
    using System.CodeDom.Compiler;
    using CodeElementAnalysis;
    using Providers;

    public class CollectionInitializerExpressionVisitor : IVisitor<CollectionInitializerExpression>
    {
        public void Visit(CollectionInitializerExpression element, IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.WriteLine(".__Initialize({");
            textWriter.Indent++;

            foreach (var containedElement in element.ContainedElements)
            {
                VisitorList.Visit(containedElement);
                textWriter.WriteLine(",");
            }

            textWriter.Indent--;
            textWriter.Write("})");
        }
    }
}