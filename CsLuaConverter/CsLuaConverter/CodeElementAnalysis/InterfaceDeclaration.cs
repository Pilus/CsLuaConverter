namespace CsLuaConverter.CodeElementAnalysis
{
    using System;
    using System.Collections.Generic;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    public class InterfaceDeclaration : BaseElement
    {
        public Scope Scope;
        public BaseList BaseList;
        public TypeParameterList Generics;
        public IList<InterfaceElement> Elements = new List<InterfaceElement>();

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
            
            while (!this.ShouldContainerBreak(token))
            {
                if (!this.IsTokenAcceptedInContainer(token))
                {
                    throw new Exception(string.Format("Unexpected token. {0} in {1}.", token.Parent.GetKind(), this.GetType().Name));
                }

                var element = new InterfaceElement();

                token = element.Analyze(token);

                this.Elements.Add(element);

                token = token.GetNextToken();
            }

            return token;
        }

        public bool IsTokenAcceptedInContainer(SyntaxToken token)
        {
            return token.Parent.IsKind(SyntaxKind.PredefinedType) || 
                token.Parent.IsKind(SyntaxKind.MethodDeclaration) ||
                token.Parent.IsKind(SyntaxKind.IdentifierName);
        }

        public bool ShouldContainerBreak(SyntaxToken token)
        {
            return token.Parent.IsKind(SyntaxKind.InterfaceDeclaration) && token.IsKind(SyntaxKind.CloseBraceToken);
        }
    }
}