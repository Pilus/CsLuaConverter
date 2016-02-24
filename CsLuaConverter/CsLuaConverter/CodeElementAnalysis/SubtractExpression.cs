namespace CsLuaConverter.CodeElementAnalysis
{
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    public class SubtractExpression : BaseElement
    {
        public override SyntaxToken Analyze(SyntaxToken token)
        {
            ExpectKind(SyntaxKind.SubtractExpression, token.Parent.GetKind());
            ExpectKind(SyntaxKind.MinusToken, token.GetKind());

            return token;
        }
    }
}