namespace CsLuaConverter.CodeElementAnalysis
{
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    public class AsExpression : BaseElement
    {
        public override SyntaxToken Analyze(SyntaxToken token)
        {
            ExpectKind(SyntaxKind.AsExpression, token.Parent.GetKind());
            ExpectKind(SyntaxKind.AsKeyword, token.GetKind());

            return token;
        }
    }
}