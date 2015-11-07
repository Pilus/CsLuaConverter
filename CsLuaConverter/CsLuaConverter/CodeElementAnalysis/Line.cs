namespace CsLuaConverter.CodeElementAnalysis
{
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    public class Line : ContainerElement
    {
        public override bool IsTokenAcceptedInContainer(SyntaxToken token)
        {
            return true;
        }

        public override bool ShouldContainerBreak(SyntaxToken token)
        {
            return token.IsKind(SyntaxKind.SemicolonToken);
        }
    }
}