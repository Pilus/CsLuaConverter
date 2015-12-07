namespace CsLuaConverter.CodeElementAnalysis
{
    using System;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    public class InterfaceDeclaration : ContainerElement
    {
        public Scope Scope;
        public BaseList BaseList;
        public TypeParameterList Generics;

        public override SyntaxToken Analyze(SyntaxToken token)
        {
            ExpectKind(SyntaxKind.InterfaceDeclaration, token.Parent.GetKind());
            this.Scope = (Scope) Enum.Parse(typeof (Scope), token.Text, true);

            token = token.GetNextToken();
            ExpectKind(SyntaxKind.InterfaceDeclaration, token.Parent.GetKind());
            ExpectKind(SyntaxKind.InterfaceKeyword, token.GetKind());

            token = token.GetNextToken();
            ExpectKind(SyntaxKind.InterfaceDeclaration, token.Parent.GetKind());
            ExpectKind(SyntaxKind.IdentifierToken, token.GetKind());

            token = token.GetNextToken();
            if (token.Parent.IsKind(SyntaxKind.TypeParameterList))
            {
                this.Generics = new TypeParameterList();
                token = this.Generics.Analyze(token);
                token = token.GetNextToken();
            }

            if (token.Parent.IsKind(SyntaxKind.BaseList))
            {
                this.BaseList = new BaseList();
                token = this.BaseList.Analyze(token);
            }
            
            ExpectKind(SyntaxKind.InterfaceDeclaration, token.Parent.GetKind());
            ExpectKind(SyntaxKind.OpenBraceToken, token.GetKind());

            token = token.GetNextToken();
            return base.Analyze(token);
        }

        public override bool IsTokenAcceptedInContainer(SyntaxToken token)
        {
            throw new System.NotImplementedException();
        }

        public override bool ShouldContainerBreak(SyntaxToken token)
        {
            return token.Parent.IsKind(SyntaxKind.InterfaceDeclaration) && token.IsKind(SyntaxKind.CloseBraceToken);
        }
    }
}