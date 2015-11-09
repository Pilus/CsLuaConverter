namespace CsLuaConverter.CodeElementAnalysis
{
    using System;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    public class FieldDeclaration : BaseElement
    {
        public Scope Scope;
        public bool Static;
        public BaseElement Type;
        public string Name;
        public BaseElement Value;

        public override SyntaxToken Analyze(SyntaxToken token)
        {
            ExpectKind(SyntaxKind.FieldDeclaration, token.Parent.GetKind());
            this.Scope = (Scope)Enum.Parse(typeof(Scope), token.Text, true);
            token = token.GetNextToken();

            if (token.IsKind(SyntaxKind.StaticKeyword))
            {
                this.Static = true;
                token = token.GetNextToken();
            }

            this.Type = GenerateMatchingElement(token);
            token = this.Type.Analyze(token);
            token = token.GetNextToken();

            ExpectKind(SyntaxKind.VariableDeclarator, token.Parent.GetKind());
            ExpectKind(SyntaxKind.IdentifierToken, token.GetKind());
            this.Name = token.Text;
            token = token.GetNextToken();

            if (token.Parent.IsKind(SyntaxKind.EqualsValueClause))
            {
                token = token.GetNextToken();
                this.Value = GenerateMatchingElement(token);
                token = this.Value.Analyze(token);
                token = token.GetNextToken();
            }

            ExpectKind(SyntaxKind.SemicolonToken, token.GetKind());

            return token;
        }
    }
}