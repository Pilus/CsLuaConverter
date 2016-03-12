namespace CsLuaConverter.CodeElementAnalysis.Statements
{
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    public class ThrowStatement : BaseStatement
    {
        public Expression Expression;
        public override SyntaxToken Analyze(SyntaxToken token)
        {
            ExpectKind(SyntaxKind.ThrowStatement, token.Parent.GetKind());
            ExpectKind(SyntaxKind.ThrowKeyword, token.GetKind());

            token = token.GetNextToken();
            this.Expression = new Expression();
            token = this.Expression.Analyze(token);

            ExpectKind(SyntaxKind.ThrowStatement, token.Parent.GetKind());
            ExpectKind(SyntaxKind.SemicolonToken, token.GetKind());
            return token;
        }
    }
}