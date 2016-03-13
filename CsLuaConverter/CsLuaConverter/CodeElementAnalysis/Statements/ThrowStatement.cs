namespace CsLuaConverter.CodeElementAnalysis.Statements
{
    using Expressions;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    public class ThrowStatement : BaseStatement
    {
        public ExpressionBase Expression;
        public override SyntaxToken Analyze(SyntaxToken token)
        {
            ExpectKind(SyntaxKind.ThrowStatement, token.Parent.GetKind());
            ExpectKind(SyntaxKind.ThrowKeyword, token.GetKind());

            token = token.GetNextToken();
            this.Expression = ExpressionBase.CreateExpression(token);
            token = this.Expression.Analyze(token);

            ExpectKind(SyntaxKind.ThrowStatement, token.Parent.GetKind());
            ExpectKind(SyntaxKind.SemicolonToken, token.GetKind());
            return token;
        }
    }
}