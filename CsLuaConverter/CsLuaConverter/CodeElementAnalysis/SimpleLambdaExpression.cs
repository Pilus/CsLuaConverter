namespace CsLuaConverter.CodeElementAnalysis
{
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    public class SimpleLambdaExpression : BaseElement
    {
        public override SyntaxToken Analyze(SyntaxToken token)
        {
            ExpectKind(SyntaxKind.SimpleLambdaExpression, token.Parent.GetKind());
            ExpectKind(SyntaxKind.EqualsGreaterThanToken, token.GetKind());

            return token;
        }
    }
}