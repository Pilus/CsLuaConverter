namespace CsLuaConverter.CodeElementAnalysis
{
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    public class TrueLiteralExpression : BaseElement
    {
        public override SyntaxToken Analyze(SyntaxToken token)
        {
            ExpectKind(SyntaxKind.TrueLiteralExpression, token.Parent.GetKind());
            ExpectKind(SyntaxKind.TrueKeyword, token.GetKind());
            return token;
        }
    }
}