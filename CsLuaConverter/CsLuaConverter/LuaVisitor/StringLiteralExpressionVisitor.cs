namespace CsLuaConverter.LuaVisitor
{
    using System.CodeDom.Compiler;
    using CodeElementAnalysis;
    using Providers;

    public class StringLiteralExpressionVisitor : IVisitor<StringLiteralExpression>
    {
        public void Visit(StringLiteralExpression element, IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.Write(element.Text);
        }
    }
}