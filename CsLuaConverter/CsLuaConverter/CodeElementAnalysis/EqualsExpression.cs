namespace CsLuaConverter.CodeElementAnalysis
{
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    public class EqualsExpression : BaseElement
    {
        public override SyntaxToken Analyze(SyntaxToken token)
        {
            ExpectKind(SyntaxKind.EqualsExpression, token.Parent.GetKind());
            ExpectKind(SyntaxKind.EqualsEqualsToken, token.GetKind());

            return token;
        }
    }
}