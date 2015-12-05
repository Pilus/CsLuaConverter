namespace CsLuaConverter.CodeElementAnalysis
{
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    public class BaseExpression : BaseElement
    {
        public override SyntaxToken Analyze(SyntaxToken token)
        {
            ExpectKind(SyntaxKind.BaseExpression, token.Parent.GetKind());
            ExpectKind(SyntaxKind.BaseKeyword, token.GetKind());

            return token;
        }
    }
}