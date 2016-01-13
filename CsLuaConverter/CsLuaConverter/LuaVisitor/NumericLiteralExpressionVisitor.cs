namespace CsLuaConverter.LuaVisitor
{
    using CodeElementAnalysis;
    using System;
    using Providers;
    using System.CodeDom.Compiler;

    public class NumericLiteralExpressionVisitor : BaseOpenCloseVisitor<NumericLiteralExpression>
    {
        protected override void Write(NumericLiteralExpression element, IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.Write(element.Text);
        }
    }
}