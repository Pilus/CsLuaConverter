namespace CsLuaConverter.LuaVisitor
{
    using CodeElementAnalysis;
    using System;
    using Providers;
    using System.CodeDom.Compiler;

    public class NumericLiteralExpressionVisitor : IVisitor<NumericLiteralExpression>
    {
        public void Visit(NumericLiteralExpression element, IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.Write(element.Text);
        }
    }
}