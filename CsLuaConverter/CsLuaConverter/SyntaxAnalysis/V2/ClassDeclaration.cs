namespace CsLuaConverter.SyntaxAnalysis.V2
{
    using System;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    public class ClassDeclaration : ContainerElement
    {
        public bool IsStatic;
        private string name;
        private BaseListElement baseList;

        public override SyntaxToken Analyze(SyntaxToken token)
        {
            while (token.Parent is ClassDeclarationSyntax && token.Text != "{")
            {
                if (token.IsKind(SyntaxKind.IdentifierToken)) this.name = token.Text;
                if (token.IsKind(SyntaxKind.StaticKeyword)) this.IsStatic = true;
                token = token.GetNextToken();
            }

            if (token.Parent.IsKind(SyntaxKind.BaseList))
            {
                this.baseList = new BaseListElement();
                token = this.baseList.Analyze(token);
            }

            ExpectSyntax(token, SyntaxKind.OpenBraceToken);

            throw new NotImplementedException();
        }

        public override bool IsTokenAcceptedInContainer(SyntaxToken token)
        {
            throw new System.NotImplementedException();
        }

        public override bool ShouldContainerBreak(SyntaxToken token)
        {
            throw new System.NotImplementedException();
        }
    }
}