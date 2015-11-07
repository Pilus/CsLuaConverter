namespace CsLuaConverter.CodeElementAnalysis
{
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    public class SimpleMemberAccessExpression : BaseElement
    {
        public override SyntaxToken Analyze(SyntaxToken token)
        {
            ExpectKind(SyntaxKind.SimpleMemberAccessExpression, token.Parent.GetKind());
            ExpectKind(SyntaxKind.DotToken, token.GetKind());
            return token;
        }
    }
}