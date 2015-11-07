namespace CsLuaConverter.CodeElementAnalysis
{
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    public class EqualsValueClause : BaseElement
    {
        public override SyntaxToken Analyze(SyntaxToken token)
        {
            ExpectKind(SyntaxKind.EqualsValueClause, token.Parent.GetKind());
            ExpectKind(SyntaxKind.EqualsToken, token.GetKind());
            return token;
        }
    }
}