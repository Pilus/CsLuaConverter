namespace CsLuaConverter.CodeElementAnalysis
{
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    public class ComplexElementInitializerExpression : BaseElement
    {
        public BaseElement KeyElement;
        public BaseElement ValueElement;

        public override SyntaxToken Analyze(SyntaxToken token)
        {
            ExpectKind(SyntaxKind.ComplexElementInitializerExpression, token.Parent.GetKind());
            ExpectKind(SyntaxKind.OpenBraceToken, token.GetKind());

            token = token.GetNextToken();
            ExpectKind(new []{ SyntaxKind.StringLiteralExpression, SyntaxKind.NumericLiteralExpression }, token.Parent.GetKind());
            this.KeyElement = GenerateMatchingElement(token);
            token = this.KeyElement.Analyze(token);
            token = token.GetNextToken();

            ExpectKind(SyntaxKind.ComplexElementInitializerExpression, token.Parent.GetKind());
            ExpectKind(SyntaxKind.CommaToken, token.GetKind());
            token = token.GetNextToken();

            this.ValueElement = GenerateMatchingElement(token);
            token = this.ValueElement.Analyze(token);
            token = token.GetNextToken();

            ExpectKind(SyntaxKind.ComplexElementInitializerExpression, token.Parent.GetKind());
            ExpectKind(SyntaxKind.CloseBraceToken, token.GetKind());
            return token;
        }
    }
}