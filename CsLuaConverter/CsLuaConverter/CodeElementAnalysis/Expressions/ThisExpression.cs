namespace CsLuaConverter.CodeElementAnalysis.Expressions
{
    using Microsoft.CodeAnalysis;

    public class ThisExpression : ExpressionBase
    {
        public ThisExpression(SyntaxNode node) : base(node)
        {
        }

        protected override SyntaxToken InnerAnalyze(SyntaxToken token)
        {
            return token;
        }
    }
}