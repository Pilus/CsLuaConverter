namespace CsLuaConverter.CodeElementAnalysis
{
    using System;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    public class ObjectCreationExpression : BaseElement
    {
        public BaseElement TypeElement;
        public ArgumentList ArgumentList;

        public override SyntaxToken Analyze(SyntaxToken token)
        {
            ExpectKind(SyntaxKind.ObjectCreationExpression, token.Parent.GetKind());
            ExpectKind(SyntaxKind.NewKeyword, token.GetKind());
            token = token.GetNextToken();

            if (token.Parent.IsKind(SyntaxKind.IdentifierName))
            {
                this.TypeElement = new IdentifierName();
            }
            else if(token.Parent.IsKind(SyntaxKind.GenericName))
            {
                this.TypeElement = new GenericName();
            }
            else
            {
                throw new Exception("Unexpected element. " + token.Parent.GetKind());
            }
            
            return this.TypeElement.Analyze(token);
        }
    }
}