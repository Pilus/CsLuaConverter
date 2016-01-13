namespace CsLuaConverter.LuaVisitor
{
    using System.CodeDom.Compiler;
    using CodeElementAnalysis;
    using Providers;

    public class SimpleMemberAccessExpressionVisitor : IOpenCloseVisitor<SimpleMemberAccessExpression>
    {
        public void Visit(SimpleMemberAccessExpression element, IndentedTextWriter textWriter, IProviders providers)
        {
            throw new LuaVisitorException("SimpleMemberAccessExpression should be visited trough the open / close visitor of its parent.");
        }

        public void WriteOpen(SimpleMemberAccessExpression element, IndentedTextWriter textWriter, IProviders providers)
        {
            VisitorList.WriteOpen(element.InnerElement);
        }

        public void WriteClose(SimpleMemberAccessExpression element, IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.Write(".");
            VisitorList.WriteClose(element.InnerElement);
        }
    }
}