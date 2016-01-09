namespace CsLuaConverter.LuaVisitor
{
    using System.CodeDom.Compiler;
    using CodeElementAnalysis;
    using Providers;

    public class SimpleMemberAccessExpressionVisitor : IVisitor<SimpleMemberAccessExpression>
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

        public static void WriteOpen(SimpleMemberAccessExpression element, IndentedTextWriter textWriter, IProviders providers)
        {
            if (element.InnerElement is IdentifierName)
            {
                IdentifierNameVisitor.WriteOpen((IdentifierName) element.InnerElement, textWriter, providers, true);
            }
        }

        public static void WriteClose(SimpleMemberAccessExpression element, IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.Write(".");

            if (element.InnerElement is IdentifierName)
            {
                IdentifierNameVisitor.WriteClose((IdentifierName)element.InnerElement, textWriter, providers, true);
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
    }
}