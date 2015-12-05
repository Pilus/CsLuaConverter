namespace CsLuaConverter.CodeElementAnalysis
{
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    public class CastExpression : BaseElement
    {
        public IdentifierName Type;

        public override SyntaxToken Analyze(SyntaxToken token)
        {
            ExpectKind(SyntaxKind.CastExpression, token.Parent.GetKind());
            ExpectKind(SyntaxKind.OpenParenToken, token.GetKind());
            token = token.GetNextToken();

            ExpectKind(SyntaxKind.IdentifierName, token.Parent.GetKind());
            ExpectKind(SyntaxKind.IdentifierToken, token.GetKind());
            this.Type = new IdentifierName();
            token = token.GetNextToken();

            ExpectKind(SyntaxKind.CastExpression, token.Parent.GetKind());
            ExpectKind(SyntaxKind.CloseParenToken, token.GetKind());

            return token;
        }
    }
}