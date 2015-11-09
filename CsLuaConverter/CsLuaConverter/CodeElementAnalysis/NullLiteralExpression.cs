namespace CsLuaConverter.CodeElementAnalysis
{
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    public class NullLiteralExpression : BaseElement
    {
        public override SyntaxToken Analyze(SyntaxToken token)
        {
            ExpectKind(SyntaxKind.NullLiteralExpression, token.Parent.GetKind());
            ExpectKind(SyntaxKind.NullKeyword, token.GetKind());
            return token;
        }
    }
}