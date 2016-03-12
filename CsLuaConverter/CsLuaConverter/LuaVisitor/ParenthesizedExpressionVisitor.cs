namespace CsLuaConverter.LuaVisitor
{
    using System.CodeDom.Compiler;
    using CodeElementAnalysis;
    using Providers;

    public class ParenthesizedExpressionVisitor : BaseOpenCloseVisitor<ParenthesizedExpression>
    {
        protected  override void Write(ParenthesizedExpression element, IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.Write("(");
            VisitorList.Visit(element.Expression);
            textWriter.Write(")");
        }
    }
}