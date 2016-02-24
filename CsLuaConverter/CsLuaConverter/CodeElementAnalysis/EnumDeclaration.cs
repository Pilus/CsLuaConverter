namespace CsLuaConverter.CodeElementAnalysis
{
    using System;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    public class EnumDeclaration : ContainerElement
    {
        public Scope Scope;
        public string Name;

        public override SyntaxToken Analyze(SyntaxToken token)
        {
            ExpectKind(SyntaxKind.EnumDeclaration, token.Parent.GetKind());
            this.Scope = (Scope) Enum.Parse(typeof (Scope), token.Text, true);

            token = token.GetNextToken();
            ExpectKind(SyntaxKind.EnumDeclaration, token.Parent.GetKind());
            ExpectKind(SyntaxKind.EnumKeyword, token.GetKind());

            token = token.GetNextToken();
            ExpectKind(SyntaxKind.EnumDeclaration, token.Parent.GetKind());
            ExpectKind(SyntaxKind.IdentifierToken, token.GetKind());
            this.Name = token.Text;

            token = token.GetNextToken();
            ExpectKind(SyntaxKind.EnumDeclaration, token.Parent.GetKind());
            ExpectKind(SyntaxKind.OpenBraceToken, token.GetKind());

            token = token.GetNextToken();
            return base.Analyze(token);
        }

        public override bool IsTokenAcceptedInContainer(SyntaxToken token)
        {
            return token.Parent.IsKind(SyntaxKind.EnumMemberDeclaration);
        }

        public override bool ShouldContainerBreak(SyntaxToken token)
        {
            return token.Parent.IsKind(SyntaxKind.EnumDeclaration) && token.IsKind(SyntaxKind.CloseBraceToken);
        }

        public override bool IsDelimiter(SyntaxToken token)
        {
            return token.Parent.IsKind(SyntaxKind.EnumDeclaration) && token.IsKind(SyntaxKind.CommaToken);
        }
    }
}