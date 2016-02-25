namespace CsLuaConverter.CodeElementAnalysis
{
    using System.Collections.Generic;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    public class IdentifierName : ElementWithInnerElement
    {
        public readonly List<string> Names = new List<string>();

        public override SyntaxToken Analyze(SyntaxToken token)
        {
            ExpectKind(SyntaxKind.IdentifierName, token.Parent.GetKind());
            
            while (token.Parent.IsKind(SyntaxKind.IdentifierName))
            {
                this.Names.Add(token.Text);

                token = token.GetNextToken();

                if (token.Parent.IsKind(SyntaxKind.QualifiedName) && token.IsKind(SyntaxKind.DotToken))
                {
                    token = token.GetNextToken();
                }
            }

            if (token.Is(SyntaxKind.SimpleMemberAccessExpression, SyntaxKind.DotToken))
            {
                this.InnerElement = new SimpleMemberAccessExpression();
                token = this.InnerElement.Analyze(token);
                return token;
            }

            if (token.Is(SyntaxKind.BracketedArgumentList, SyntaxKind.OpenBracketToken))
            {
                this.InnerElement = new BracketedArgumentList();
                token = this.InnerElement.Analyze(token);
                return token;
            }

            if (token.Is(SyntaxKind.ArgumentList, SyntaxKind.OpenParenToken))
            {
                this.InnerElement = new ArgumentList();
                token = this.InnerElement.Analyze(token);
                return token;
            }

            if (token.Is(SyntaxKind.GenericName, SyntaxKind.IdentifierToken))
            {
                this.InnerElement = new GenericName();
                token = this.InnerElement.Analyze(token);
                return token;
            }

            return token.GetPreviousToken();
        }
    }
}