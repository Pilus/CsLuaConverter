namespace CsLuaConverter.CodeElementAnalysis
{
    using System;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    public class PropertyDeclaration : BaseElement
    {
        public Scope Scope;
        public BaseElement Type;
        public string Name;
        public AccessorList Accessors;

        public override SyntaxToken Analyze(SyntaxToken token)
        {
            ExpectKind(SyntaxKind.PropertyDeclaration, token.Parent.GetKind());
            this.Scope = (Scope)Enum.Parse(typeof (Scope), token.Text, true);

            token = token.GetNextToken();
            this.Type = GenerateMatchingElement(token);
            token = this.Type.Analyze(token);

            token = token.GetNextToken();
            ExpectKind(SyntaxKind.PropertyDeclaration, token.Parent.GetKind());
            ExpectKind(SyntaxKind.IdentifierToken, token.GetKind());
            this.Name = token.Text;

            token = token.GetNextToken();
            this.Accessors = new AccessorList();
            return this.Accessors.Analyze(token);
        }
    }
}