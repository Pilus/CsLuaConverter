namespace CsLuaConverter.CodeElementAnalysis
{
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    public class AttributeArgumentList : ContainerElement
    {
        public override SyntaxToken Analyze(SyntaxToken token)
        {
            ExpectKind(SyntaxKind.AttributeArgumentList, token.Parent.GetKind());
            ExpectKind(SyntaxKind.OpenParenToken, token.GetKind());

            return base.Analyze(token);
        }

        public override bool IsTokenAcceptedInContainer(SyntaxToken token)
        {
            return token.Parent.IsKind(SyntaxKind.StringLiteralExpression) || 
                   token.Parent.IsKind(SyntaxKind.NumericLiteralExpression) ||
                   token.Parent.IsKind(SyntaxKind.IdentifierName) ||
                   token.Parent.IsKind(SyntaxKind.NameEquals);
        }

        public override bool ShouldContainerBreak(SyntaxToken token)
        {
            return token.Parent.IsKind(SyntaxKind.AttributeArgumentList) && token.IsKind(SyntaxKind.CloseParenToken);
        }

        public override bool IsDelimiter(SyntaxToken token)
        {
            return token.Parent.IsKind(SyntaxKind.AttributeArgumentList) && (token.IsKind(SyntaxKind.CommaToken) || token.IsKind(SyntaxKind.OpenParenToken));
        }
    }
}