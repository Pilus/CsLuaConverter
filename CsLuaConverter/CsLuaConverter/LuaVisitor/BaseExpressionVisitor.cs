namespace CsLuaConverter.LuaVisitor
{
    using System.CodeDom.Compiler;
    using CodeElementAnalysis;
    using Providers;

    public class BaseExpressionVisitor : BaseOpenCloseVisitor<BaseExpression>
    {
        protected override void Write(BaseExpression element, IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.Write("baseElement");
        }
    }
}