namespace CsLuaConverter.CodeElementAnalysis
{
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    public class ArgumentList : DelimiteredContainerElement
    {
        public BaseElement InnerElement;

        public override SyntaxToken Analyze(SyntaxToken token)
        {
            ExpectKind(SyntaxKind.ArgumentList, token.Parent.GetKind());
            ExpectKind(SyntaxKind.OpenParenToken, token.GetKind());
            token = token.GetNextToken();
            token = base.Analyze(token);

            token = token.GetNextToken();

            if (token.Is(SyntaxKind.SimpleMemberAccessExpression, SyntaxKind.DotToken) ||
                token.Is(SyntaxKind.BracketedArgumentList, SyntaxKind.OpenBracketToken) ||
                token.Is(SyntaxKind.ArgumentList, SyntaxKind.OpenParenToken) ||
                token.Is(SyntaxKind.ObjectInitializerExpression, SyntaxKind.OpenBraceToken) ||
                token.Is(SyntaxKind.ArrayInitializerExpression, SyntaxKind.OpenBraceToken) ||
                token.Is(SyntaxKind.CollectionInitializerExpression, SyntaxKind.OpenBraceToken) ||
                token.Is(SyntaxKind.ComplexElementInitializerExpression, SyntaxKind.OpenBraceToken))
            {
                this.InnerElement = GenerateMatchingElement(token);
                token = this.InnerElement.Analyze(token);
                return token;
            }

            return token.GetPreviousToken();
        }

        public override bool IsTokenAcceptedInContainer(SyntaxToken token)
        {
            return true;
        }

        public override bool ShouldContainerBreak(SyntaxToken token)
        {
            return token.Parent.IsKind(SyntaxKind.ArgumentList) && token.IsKind(SyntaxKind.CloseParenToken);
        }

        public override bool IsDelimiter(SyntaxToken token)
        {
            return token.Parent.IsKind(SyntaxKind.ArgumentList) && token.IsKind(SyntaxKind.CommaToken);
        }
    }
}