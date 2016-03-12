namespace CsLuaConverter.CodeElementAnalysis
{
    using System;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Statements;

    public class ObjectCreationExpression : BaseStatement
    {
        public BaseElement TypeElement;

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