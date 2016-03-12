namespace CsLuaConverter.CodeElementAnalysis
{
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    public class Expression : ContainerElement
    {
        public override SyntaxToken Analyze(SyntaxToken token)
        {
            token = base.Analyze(token);
            return token;
        }

        public override bool IsTokenAcceptedInContainer(SyntaxToken token)
        {
            return true;
        }

        public override bool ShouldContainerBreak(SyntaxToken token)
        {
            return
                token.IsKind(SyntaxKind.CommaToken) ||
                token.IsKind(SyntaxKind.CloseBraceToken) ||
                token.IsKind(SyntaxKind.CloseParenToken) ||
                token.IsKind(SyntaxKind.SemicolonToken) ||
                token.IsKind(SyntaxKind.EqualsToken);
        }
    }
}