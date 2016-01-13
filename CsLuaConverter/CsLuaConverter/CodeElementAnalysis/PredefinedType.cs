namespace CsLuaConverter.CodeElementAnalysis
{
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    public class PredefinedType : ElementWithInnerElement
    {
        public string Text;
        public bool IsArray;

        public override SyntaxToken Analyze(SyntaxToken token)
        {
            ExpectKind(SyntaxKind.PredefinedType, token.Parent.GetKind());
            this.Text = token.Text;

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