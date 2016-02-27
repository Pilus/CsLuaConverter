namespace CsLuaConverter.CodeElementAnalysis
{
    using System;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Statements;

    public class FieldDeclaration : BaseElement
    {
        public Scope Scope;
        public bool Static;
        public bool Const;
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

            if (token.IsKind(SyntaxKind.ConstKeyword))
            {
                this.Const = true;
                token = token.GetNextToken();
            }

            if (token.IsKind(SyntaxKind.ReadOnlyKeyword))
            {
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
                var statement = new Statement();
                token = statement.Analyze(token);
                statement.EndToken = "";
                this.Value = statement;
            }

            ExpectKind(SyntaxKind.SemicolonToken, token.GetKind());

            return token;
        }
    }
}