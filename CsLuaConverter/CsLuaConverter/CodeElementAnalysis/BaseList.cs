namespace CsLuaConverter.CodeElementAnalysis
{
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    public class BaseList : ContainerElement
    {
        public override SyntaxToken Analyze(SyntaxToken token)
        {
            ExpectKind(SyntaxKind.BaseList, token.Parent.GetKind());
            ExpectKind(SyntaxKind.ColonToken, token.GetKind());

            while (token.Parent.IsKind(SyntaxKind.BaseList))
            {
                token = token.GetNextToken();
                token = base.Analyze(token);
            }

            return token;
        }

        public override bool IsTokenAcceptedInContainer(SyntaxToken token)
        {
            return token.Parent.IsKind(SyntaxKind.IdentifierName) 
                || token.Parent.IsKind(SyntaxKind.GenericName);
        }

        public override bool ShouldContainerBreak(SyntaxToken token)
        {
            return !this.IsTokenAcceptedInContainer(token);
        }
    }
}