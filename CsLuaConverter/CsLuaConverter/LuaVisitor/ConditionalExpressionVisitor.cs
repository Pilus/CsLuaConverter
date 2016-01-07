namespace CsLuaConverter.LuaVisitor
{
    using System.CodeDom.Compiler;
    using CodeElementAnalysis;
    using Providers;

    public class ConditionalExpressionVisitor : IVisitor<ConditionalExpression>
    {
        public void Visit(ConditionalExpression element, IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.Write(element.Text.Equals("?") ? " and " : " or ");
        }
    }
}