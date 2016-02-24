namespace CsLuaConverter.CodeElementAnalysis
{
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    public class IsExpression : BaseElement
    {
        public override SyntaxToken Analyze(SyntaxToken token)
        {
            ExpectKind(SyntaxKind.IsExpression, token.Parent.GetKind());
            ExpectKind(SyntaxKind.IsKeyword, token.GetKind());

            return token;
        }
    }
}