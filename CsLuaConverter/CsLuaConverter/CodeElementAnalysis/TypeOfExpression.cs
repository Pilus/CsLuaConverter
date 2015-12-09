namespace CsLuaConverter.CodeElementAnalysis
{
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    public class TypeOfExpression : ContainerElement
    {
        public override SyntaxToken Analyze(SyntaxToken token)
        {
            ExpectKind(SyntaxKind.TypeOfExpression, token.Parent.GetKind());
            ExpectKind(SyntaxKind.TypeOfKeyword, token.GetKind());

            token = token.GetNextToken();
            ExpectKind(SyntaxKind.TypeOfExpression, token.Parent.GetKind());
            ExpectKind(SyntaxKind.OpenParenToken, token.GetKind());

            token = token.GetNextToken();
            return base.Analyze(token);
        }

        public override bool IsTokenAcceptedInContainer(SyntaxToken token)
        {
            return token.Parent.IsKind(SyntaxKind.IdentifierName);
        }

        public override bool ShouldContainerBreak(SyntaxToken token)
        {
            return token.Parent.IsKind(SyntaxKind.TypeOfExpression) && token.IsKind(SyntaxKind.CloseParenToken);
        }
    }
}