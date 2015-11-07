namespace CsLuaConverter.CodeElementAnalysis
{
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    public class PredefinedType : BaseElement
    {
        public string Text;

        public override SyntaxToken Analyze(SyntaxToken token)
        {
            ExpectKind(SyntaxKind.PredefinedType, token.Parent.GetKind());
            this.Text = token.Text;

            return token;
        }
    }
}