namespace CsLuaConverter.CodeElementAnalysis
{
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    public class NameEquals : BaseElement
    {
        public override SyntaxToken Analyze(SyntaxToken token)
        {
            ExpectKind(SyntaxKind.NameEquals, token.Parent.GetKind());
            ExpectKind(SyntaxKind.EqualsToken, token.GetKind());

            return token;
        }
    }
}