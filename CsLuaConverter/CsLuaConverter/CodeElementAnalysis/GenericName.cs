namespace CsLuaConverter.CodeElementAnalysis
{
    using System.Collections.Generic;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    public class GenericName : BaseElement
    {
        public string Name;
        public TypeArgumentList ArgumentList;
        public bool IsArray;
        public BaseElement InnerElement;

        public override SyntaxToken Analyze(SyntaxToken token)
        {
            ExpectKind(SyntaxKind.GenericName, token.Parent.GetKind());
            ExpectKind(SyntaxKind.IdentifierToken, token.GetKind());
            this.Name = token.Text;
            token = token.GetNextToken();

            this.ArgumentList = new TypeArgumentList();
            token = this.ArgumentList.Analyze(token);

            if (token.GetNextToken().Parent.IsKind(SyntaxKind.ArrayRankSpecifier))
            {
                token = token.GetNextToken();
                ExpectKind(SyntaxKind.OpenBracketToken, token.GetKind());
                token = token.GetNextToken();
                ExpectKind(SyntaxKind.ArrayRankSpecifier, token.Parent.GetKind());
                ExpectKind(SyntaxKind.CloseBracketToken, token.GetKind());
                this.IsArray = true;
            }

            token = token.GetNextToken();

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

            return token.GetPreviousToken();
        }
    }
}