namespace CsLuaConverter.CodeElementAnalysis
{
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    public class ComplexElementInitializerExpression : ContainerElement
    {
        public override SyntaxToken Analyze(SyntaxToken token)
        {
            ExpectKind(SyntaxKind.ComplexElementInitializerExpression, token.Parent.GetKind());
            ExpectKind(SyntaxKind.OpenBraceToken, token.GetKind());

            token = token.GetNextToken();
            return base.Analyze(token);
        }

        public override bool IsTokenAcceptedInContainer(SyntaxToken token)
        {
            return token.Parent.IsKind(SyntaxKind.StringLiteralExpression) ||
                   token.Parent.IsKind(SyntaxKind.NumericLiteralExpression);
        }

        public override bool ShouldContainerBreak(SyntaxToken token)
        {
            return token.Parent.IsKind(SyntaxKind.ComplexElementInitializerExpression) &&
                   token.IsKind(SyntaxKind.CloseBraceToken);
        }

        public override bool IsDelimiter(SyntaxToken token)
        {
            return token.Parent.IsKind(SyntaxKind.ComplexElementInitializerExpression) &&
                   token.IsKind(SyntaxKind.CommaToken);
        }
    }
}