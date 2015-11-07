namespace CsLuaConverter.CodeElementAnalysis
{
    using System;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    public class ObjectCreationExpression : BaseElement
    {
        public IdentifierName TypeElement;
        public ArgumentList ArgumentList;

        public override SyntaxToken Analyze(SyntaxToken token)
        {
            ExpectKind(SyntaxKind.ObjectCreationExpression, token.Parent.GetKind());
            ExpectKind(SyntaxKind.NewKeyword, token.GetKind());
            token = token.GetNextToken();

            this.TypeElement = new IdentifierName();
            token = this.TypeElement.Analyze(token);
            token = token.GetNextToken();

            this.ArgumentList = new ArgumentList();
            token = this.ArgumentList.Analyze(token);

            return token;
        }
    }
}