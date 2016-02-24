namespace CsLuaConverter.CodeElementAnalysis
{
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    public class UnaryMinusExpression : BaseElement
    {
        public override SyntaxToken Analyze(SyntaxToken token)
        {
            ExpectKind(SyntaxKind.UnaryMinusExpression, token.Parent.GetKind());
            return token;
        }
    }
}