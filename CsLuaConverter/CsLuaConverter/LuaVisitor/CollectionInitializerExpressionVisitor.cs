namespace CsLuaConverter.LuaVisitor
{
    using System.CodeDom.Compiler;
    using CodeElementAnalysis;
    using Providers;

    public class CollectionInitializerExpressionVisitor : IOpenCloseVisitor<CollectionInitializerExpression>
    {
        public void Visit(CollectionInitializerExpression element, IndentedTextWriter textWriter, IProviders providers)
        {
            this.WriteOpen(element, textWriter, providers);
            this.WriteClose(element, textWriter, providers);
        }

        public void WriteOpen(CollectionInitializerExpression element, IndentedTextWriter textWriter, IProviders providers)
        {
        }

        public void WriteClose(CollectionInitializerExpression element, IndentedTextWriter textWriter, IProviders providers)
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