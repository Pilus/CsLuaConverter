namespace CsLuaConverter.CodeElementAnalysis
{
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    public class CastExpression : BaseElement
    {
        public BaseElement Type;

        public override SyntaxToken Analyze(SyntaxToken token)
        {
            ExpectKind(SyntaxKind.CastExpression, token.Parent.GetKind());
            ExpectKind(SyntaxKind.OpenParenToken, token.GetKind());
            token = token.GetNextToken();

            this.Type = GenerateMatchingElement(token);
            token = token.GetNextToken();

            ExpectKind(SyntaxKind.CastExpression, token.Parent.GetKind());
            ExpectKind(SyntaxKind.CloseParenToken, token.GetKind());

            return token;
        }
    }
}