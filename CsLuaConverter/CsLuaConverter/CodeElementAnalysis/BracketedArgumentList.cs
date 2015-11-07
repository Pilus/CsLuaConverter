namespace CsLuaConverter.CodeElementAnalysis
{
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    public class BracketedArgumentList : ContainerElement
    {
        public override SyntaxToken Analyze(SyntaxToken token)
        {
            ExpectKind(SyntaxKind.BracketedArgumentList, token.Parent.GetKind());
            ExpectKind(SyntaxKind.OpenBracketToken, token.GetKind());
            token = token.GetNextToken();
            return base.Analyze(token);
        }

        public override bool IsTokenAcceptedInContainer(SyntaxToken token)
        {
            return true;
        }

        public override bool ShouldContainerBreak(SyntaxToken token)
        {
            return token.Parent.IsKind(SyntaxKind.BracketedArgumentList) && token.IsKind(SyntaxKind.CloseBracketToken);
        }
    }
}