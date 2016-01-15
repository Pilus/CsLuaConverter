namespace CsLuaConverter.CodeElementAnalysis
{
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    public class ThisExpression : ElementWithInnerElement
    {
        public override SyntaxToken Analyze(SyntaxToken token)
        {
            ExpectKind(SyntaxKind.ThisExpression, token.Parent.GetKind());
            ExpectKind(SyntaxKind.ThisKeyword, token.GetKind());
            token = token.GetNextToken();

            ExpectKind(SyntaxKind.SimpleMemberAccessExpression, token.Parent.GetKind());
            ExpectKind(SyntaxKind.DotToken, token.GetKind());
            token = token.GetNextToken();

            this.InnerElement = GenerateMatchingElement(token);

            return this.InnerElement.Analyze(token);
        }
    }
}