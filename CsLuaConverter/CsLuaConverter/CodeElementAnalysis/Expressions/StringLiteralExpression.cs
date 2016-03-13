namespace CsLuaConverter.CodeElementAnalysis.Expressions
{
    using Microsoft.CodeAnalysis;

    public class StringLiteralExpression : ExpressionBase
    {
        public string Text;
        public StringLiteralExpression(SyntaxNode node) : base(node)
        {
        }

        protected override SyntaxToken InnerAnalyze(SyntaxToken token)
        {
            this.Text = token.Text;
            return token;
        }
    }
}