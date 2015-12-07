namespace CsLuaConverter.CodeElementAnalysis
{
    using System.Collections.Generic;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;
    using SyntaxAnalysis;

    public class NamespaceDeclaration : ContainerElement
    {
        public List<string> FullName = new List<string>();

        private SyntaxToken closeBranceSyntax;

        public override SyntaxToken Analyze(SyntaxToken token)
        {
            LuaElementHelper.CheckType(typeof(NamespaceDeclarationSyntax), token.Parent);

            token = token.GetNextToken();
            LuaElementHelper.CheckType(typeof(IdentifierNameSyntax), token.Parent);
            this.FullName.Add(token.Text);

            token = token.GetNextToken();
            while (token.Parent is QualifiedNameSyntax && token.Text.Equals("."))
            {
                token = token.GetNextToken();
                LuaElementHelper.CheckType(typeof(IdentifierNameSyntax), token.Parent);
                this.FullName.Add(token.Text);
                token = token.GetNextToken();
            }

            LuaElementHelper.CheckType(typeof(NamespaceDeclarationSyntax), token.Parent);

            this.closeBranceSyntax = ((NamespaceDeclarationSyntax) token.Parent).CloseBraceToken;

            token = token.GetNextToken();

            return base.Analyze(token);
        }

        public override bool IsTokenAcceptedInContainer(SyntaxToken token)
        {
            return token.Parent.IsKind(SyntaxKind.ClassDeclaration) ||
                   token.Parent.IsKind(SyntaxKind.UsingDirective) ||
                   token.Parent.IsKind(SyntaxKind.EnumDeclaration) ||
                   token.Parent.IsKind(SyntaxKind.InterfaceDeclaration) ||
                   token.Parent.IsKind(SyntaxKind.AttributeList);
        }

        public override bool ShouldContainerBreak(SyntaxToken token)
        {
            return token == this.closeBranceSyntax;
        }
    }
}