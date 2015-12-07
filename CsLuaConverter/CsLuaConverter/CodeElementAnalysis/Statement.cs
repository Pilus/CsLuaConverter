namespace CsLuaConverter.CodeElementAnalysis
{
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    public class Statement : ContainerElement
    {
        public override bool IsTokenAcceptedInContainer(SyntaxToken token)
        {
            return !token.Parent.IsKind(SyntaxKind.Block);
        }

        public override bool ShouldContainerBreak(SyntaxToken token)
        {
            return 
                token.IsKind(SyntaxKind.CommaToken) || 
                token.IsKind(SyntaxKind.CloseBraceToken) ||
                token.IsKind(SyntaxKind.CloseParenToken) ||
                token.IsKind(SyntaxKind.SemicolonToken);
        }
    }
}