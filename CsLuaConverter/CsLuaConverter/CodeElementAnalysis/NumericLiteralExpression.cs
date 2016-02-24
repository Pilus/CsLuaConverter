namespace CsLuaConverter.CodeElementAnalysis
{
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    public class NumericLiteralExpression : ElementWithInnerElement
    {
        public string Text;

        public override SyntaxToken Analyze(SyntaxToken token)
        {
            ExpectKind(SyntaxKind.NumericLiteralExpression, token.Parent.GetKind());
            ExpectKind(SyntaxKind.NumericLiteralToken, token.GetKind());
            this.Text = token.Text;
            token = token.GetNextToken();

            if (token.Is(SyntaxKind.SimpleMemberAccessExpression, SyntaxKind.DotToken))
            {
                this.InnerElement = new SimpleMemberAccessExpression();
                token = this.InnerElement.Analyze(token);
                return token;
            }

            return token.GetPreviousToken();
        }
    }
}