namespace CsLuaConverter.CodeElementAnalysis
{
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    public class LogicalNotExpression : BaseElement
    {
        public override SyntaxToken Analyze(SyntaxToken token)
        {
            ExpectKind(SyntaxKind.LogicalNotExpression, token.Parent.GetKind());
            ExpectKind(SyntaxKind.ExclamationToken, token.GetKind());

            return token;
        }
    }
}