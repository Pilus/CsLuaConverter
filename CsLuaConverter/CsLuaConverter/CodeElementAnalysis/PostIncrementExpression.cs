namespace CsLuaConverter.CodeElementAnalysis
{
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    public class PostIncrementExpression : BaseElement
    {
        public override SyntaxToken Analyze(SyntaxToken token)
        {
            ExpectKind(SyntaxKind.PostIncrementExpression, token.Parent.GetKind());
            ExpectKind(SyntaxKind.PlusPlusToken, token.GetKind());

            return token;
        }
    }
}