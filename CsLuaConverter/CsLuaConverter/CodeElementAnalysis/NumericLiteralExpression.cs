namespace CsLuaConverter.CodeElementAnalysis
{
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    public class NumericLiteralExpression : BaseElement
    {
        public string Text;

        public override SyntaxToken Analyze(SyntaxToken token)
        {
            ExpectKind(SyntaxKind.NumericLiteralExpression, token.Parent.GetKind());
            ExpectKind(SyntaxKind.NumericLiteralToken, token.GetKind());
            this.Text = token.Text;
            return token;
        }
    }
}