namespace CsLuaConverter.CodeElementAnalysis
{
    using System;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    public class PropertyDeclaration : BaseElement
    {
        public bool Static;
        public Scope Scope;
        public BaseElement Type;
        public string Name;
        public AccessorList Accessors;

        public override SyntaxToken Analyze(SyntaxToken token)
        {
            ExpectKind(SyntaxKind.PropertyDeclaration, token.Parent.GetKind());

            this.Scope = (Scope)Enum.Parse(typeof(Scope), token.Text, true);
            token = token.GetNextToken();

            if (token.IsKind(SyntaxKind.StaticKeyword))
            {
                this.Static = true;
                token = token.GetNextToken();
            }

            if (token.Parent.IsKind(SyntaxKind.PropertyDeclaration))
            {
                throw new Exception("Expected all PropertyDeclarationTokens to be analyzed.");
            }

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