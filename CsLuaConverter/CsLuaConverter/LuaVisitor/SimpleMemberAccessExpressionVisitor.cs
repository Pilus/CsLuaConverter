namespace CsLuaConverter.LuaVisitor
{
    using System.CodeDom.Compiler;
    using CodeElementAnalysis;
    using Providers;

    public class SimpleMemberAccessExpressionVisitor : IOpenCloseVisitor<SimpleMemberAccessExpression>
    {
        public void Visit(SimpleMemberAccessExpression element, IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.Write(".");

            if (element.InnerElement is IdentifierName)
            {
                IdentifierNameVisitor.Visit((IdentifierName)element.InnerElement, textWriter, providers, true);
            }
            else if (element.InnerElement is GenericName)
            {
                GenericNameVisitor.Visit((GenericName)element.InnerElement, textWriter, providers, true);
            }
            else
            {
                throw new LuaVisitorException("Unhandled inner element of SimpleMemberAccessExpression");
            }
        }

        public void WriteOpen(SimpleMemberAccessExpression element, IndentedTextWriter textWriter, IProviders providers)
        {
            VisitorList.WriteOpen(element.InnerElement);
            textWriter.Write("(");
        }

        public void WriteClose(SimpleMemberAccessExpression element, IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.Write(" % _M.DOT)");
            textWriter.Write(".");
            VisitorList.WriteClose(element.InnerElement);
        }
    }
}