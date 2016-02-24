namespace CsLuaConverter.CodeElementAnalysis
{
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    public class PostDecrementExpression : BaseElement
    {
        public override SyntaxToken Analyze(SyntaxToken token)
        {
            ExpectKind(SyntaxKind.PostDecrementExpression, token.Parent.GetKind());
            ExpectKind(SyntaxKind.MinusMinusToken, token.GetKind());

            return token;
        }
    }
}