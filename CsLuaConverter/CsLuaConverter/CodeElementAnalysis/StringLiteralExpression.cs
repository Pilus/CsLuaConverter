namespace CsLuaConverter.CodeElementAnalysis
{
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    public class StringLiteralExpression : BaseElement
    {
        public string Text;
        public override SyntaxToken Analyze(SyntaxToken token)
        {
            ExpectKind(SyntaxKind.StringLiteralExpression, token.Parent.GetKind());
            this.Text = token.Text;
            return token;
        }
    }
}