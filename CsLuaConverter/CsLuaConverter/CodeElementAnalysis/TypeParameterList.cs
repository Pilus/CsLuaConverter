namespace CsLuaConverter.CodeElementAnalysis
{
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    public class TypeParameterList : ContainerElement
    {
        public override bool IsTokenAcceptedInContainer(SyntaxToken token)
        {
            return token.Parent.IsKind(SyntaxKind.TypeParameter);
        }

        public override bool ShouldContainerBreak(SyntaxToken token)
        {
            return token.Parent.IsKind(SyntaxKind.TypeParameterList) && token.IsKind(SyntaxKind.GreaterThanToken);
        }

        public override bool IsDelimiter(SyntaxToken token)
        {
            return token.Parent.IsKind(SyntaxKind.TypeParameterList) && (
                token.IsKind(SyntaxKind.LessThanToken) ||
                token.IsKind(SyntaxKind.CommaToken));
        }
    }
}