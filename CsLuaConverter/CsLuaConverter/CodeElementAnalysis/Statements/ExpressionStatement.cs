namespace CsLuaConverter.CodeElementAnalysis.Statements
{
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    public class ExpressionStatement : BaseStatement
    {
        public Expression Expression;
        public Expression ValueExpression;

        public override SyntaxToken Analyze(SyntaxToken token)
        {
            this.Expression = new Expression();
            token = this.Expression.Analyze(token);

            if (SyntaxKind.EqualsToken != token.GetKind())
            {
                ExpectKind(SyntaxKind.SemicolonToken, token.GetKind());
                return token;
            }

            token = token.GetNextToken();

            this.ValueExpression = new Expression();
            token = this.ValueExpression.Analyze(token);

            ExpectKind(SyntaxKind.SemicolonToken, token.GetKind());
            return token;
        }
    }
}