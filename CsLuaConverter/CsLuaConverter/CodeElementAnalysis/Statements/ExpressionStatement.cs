namespace CsLuaConverter.CodeElementAnalysis.Statements
{
    using Expressions;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    public class ExpressionStatement : BaseStatement
    {
        public ExpressionBase Expression;
        public ExpressionBase ValueExpression;

        public override SyntaxToken Analyze(SyntaxToken token)
        {
            this.Expression = ExpressionBase.CreateExpression(token);
            token = this.Expression.Analyze(token);
            token = token.GetNextToken();

            if (SyntaxKind.EqualsToken != token.GetKind())
            {
                ExpectKind(SyntaxKind.SemicolonToken, token.GetKind());
                return token;
            }

            this.ValueExpression = ExpressionBase.CreateExpression(token);
            token = this.ValueExpression.Analyze(token);

            ExpectKind(SyntaxKind.SemicolonToken, token.GetKind());
            return token;
        }
    }
}