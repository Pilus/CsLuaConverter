namespace CsLuaConverter.CodeElementAnalysis
{
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    public class NotEqualsExpression : BaseElement
    {
        public override SyntaxToken Analyze(SyntaxToken token)
        {
            ExpectKind(SyntaxKind.NotEqualsExpression, token.Parent.GetKind());
            ExpectKind(SyntaxKind.ExclamationEqualsToken, token.GetKind());

            return token;
        }
    }
}