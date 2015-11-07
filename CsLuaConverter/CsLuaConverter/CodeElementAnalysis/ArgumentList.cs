namespace CsLuaConverter.CodeElementAnalysis
{
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    public class ArgumentList : ContainerElement
    {
        public override SyntaxToken Analyze(SyntaxToken token)
        {
            ExpectKind(SyntaxKind.ArgumentList, token.Parent.GetKind());
            ExpectKind(SyntaxKind.OpenParenToken, token.GetKind());
            token = token.GetNextToken();
            return base.Analyze(token);
        }

        public override bool IsTokenAcceptedInContainer(SyntaxToken token)
        {
            return true;
        }

        public override bool ShouldContainerBreak(SyntaxToken token)
        {
            return token.Parent.IsKind(SyntaxKind.ArgumentList) && token.IsKind(SyntaxKind.CloseParenToken);
        }

        public override bool IsDelimiter(SyntaxToken token)
        {
            return token.Parent.IsKind(SyntaxKind.ArgumentList) && token.IsKind(SyntaxKind.CommaToken);
        }
    }
}