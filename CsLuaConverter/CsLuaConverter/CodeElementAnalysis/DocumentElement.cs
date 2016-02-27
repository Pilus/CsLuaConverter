namespace CsLuaConverter.CodeElementAnalysis
{
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    public class DocumentElement : ContainerElement
    {
        public override bool IsTokenAcceptedInContainer(SyntaxToken token)
        {
            return token.Parent.IsKind(SyntaxKind.NamespaceDeclaration) ||
                   token.Parent.IsKind(SyntaxKind.UsingDirective) ||
                   token.Parent.IsKind(SyntaxKind.EnumDeclaration) ||
                   token.Parent.IsKind(SyntaxKind.InterfaceDeclaration) || 
                   token.Parent.IsKind(SyntaxKind.AttributeList);
        }

        public override bool ShouldContainerBreak(SyntaxToken token)
        {
            return token.IsKind(SyntaxKind.None);
        }
    }
}