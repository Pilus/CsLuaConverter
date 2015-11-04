namespace CsLuaConverter.SyntaxAnalysis.V2
{
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    public class DocumentElement : ContainerElement
    {
        public override bool IsTokenAcceptedInContainer(SyntaxToken token)
        {
            return token.Parent.IsKind(SyntaxKind.NamespaceDeclaration);
        }

        public override bool ShouldContainerBreak(SyntaxToken token)
        {
            return false;
        }
    }
}