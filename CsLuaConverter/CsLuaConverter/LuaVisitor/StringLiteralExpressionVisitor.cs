namespace CsLuaConverter.LuaVisitor
{
    using System.CodeDom.Compiler;
    using CodeElementAnalysis;
    using Providers;

    public class StringLiteralExpressionVisitor : IVisitor<StringLiteralExpression>
    {
        public void Visit(StringLiteralExpression element, IndentedTextWriter textWriter, IProviders providers)
        {
            if (element.Text.StartsWith("@"))
            {
                textWriter.Write("[[");
                textWriter.Write(element.Text.Substring(2, element.Text.Length - 3));
                textWriter.Write("]]");
            }
            else
            {
                textWriter.Write(element.Text);
            }
        }
    }
}