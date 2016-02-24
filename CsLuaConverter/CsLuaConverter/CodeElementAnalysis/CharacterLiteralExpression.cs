namespace CsLuaConverter.CodeElementAnalysis
{
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    public class CharacterLiteralExpression : BaseElement
    {
        public string Text;

        public override SyntaxToken Analyze(SyntaxToken token)
        {
            ExpectKind(SyntaxKind.CharacterLiteralExpression, token.Parent.GetKind());
            ExpectKind(SyntaxKind.CharacterLiteralToken, token.GetKind());
            this.Text = token.Text;

            return token;
        }
    }
}