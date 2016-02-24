namespace CsLuaConverter.CodeElementAnalysis
{
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    public class FalseLiteralExpression : BaseElement
    {
        public override SyntaxToken Analyze(SyntaxToken token)
        {
            ExpectKind(SyntaxKind.FalseLiteralExpression, token.Parent.GetKind());
            ExpectKind(SyntaxKind.FalseKeyword, token.GetKind());

            return token;
        }
    }
}