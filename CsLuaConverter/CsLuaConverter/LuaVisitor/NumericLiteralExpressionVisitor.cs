namespace CsLuaConverter.LuaVisitor
{
    using System.CodeDom.Compiler;
    using CodeElementAnalysis;
    using Providers;

    public class NumericLiteralExpressionVisitor : BaseOpenCloseVisitor<NumericLiteralExpression>
    {
        protected override void Write(NumericLiteralExpression element, IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.Write(element.Text);
        }
    }
}