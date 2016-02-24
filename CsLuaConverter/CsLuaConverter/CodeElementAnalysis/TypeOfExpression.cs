namespace CsLuaConverter.CodeElementAnalysis
{
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    public class TypeOfExpression : ElementWithInnerElement
    {
        public BaseElement Type;

        public override SyntaxToken Analyze(SyntaxToken token)
        {
            ExpectKind(SyntaxKind.TypeOfExpression, token.Parent.GetKind());
            ExpectKind(SyntaxKind.TypeOfKeyword, token.GetKind());

            token = token.GetNextToken();
            ExpectKind(SyntaxKind.TypeOfExpression, token.Parent.GetKind());
            ExpectKind(SyntaxKind.OpenParenToken, token.GetKind());

            token = token.GetNextToken();
            this.Type = GenerateMatchingElement(token);
            token = this.Type.Analyze(token);

            token = token.GetNextToken();
            ExpectKind(SyntaxKind.TypeOfExpression, token.Parent.GetKind());
            ExpectKind(SyntaxKind.CloseParenToken, token.GetKind());

            token = token.GetNextToken();

            if (token.Is(SyntaxKind.SimpleMemberAccessExpression, SyntaxKind.DotToken))
            {
                this.InnerElement = new SimpleMemberAccessExpression();
                token = this.InnerElement.Analyze(token);
                return token;
            }

            return token.GetPreviousToken();
        }
    }
}