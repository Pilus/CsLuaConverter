namespace CsLuaConverter.CodeElementAnalysis
{
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    public class MultiplyExpression : BaseElement
    {
        public override SyntaxToken Analyze(SyntaxToken token)
        {
            ExpectKind(SyntaxKind.MultiplyExpression, token.Parent.GetKind());
            ExpectKind(SyntaxKind.AsteriskToken, token.GetKind());

            return token;
        }
    }
}