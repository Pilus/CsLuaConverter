namespace CsLuaConverter.CodeElementAnalysis
{
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    public class SimpleMemberAccessExpression : ElementWithInnerElement
    {
        public override SyntaxToken Analyze(SyntaxToken token)
        {
            ExpectKind(SyntaxKind.SimpleMemberAccessExpression, token.Parent.GetKind());
            ExpectKind(SyntaxKind.DotToken, token.GetKind());

            token = token.GetNextToken();

            this.InnerElement = GenerateMatchingElement(token);
            return this.InnerElement.Analyze(token);
        }
    }
}