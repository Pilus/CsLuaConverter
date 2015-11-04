namespace CsLuaConverter.SyntaxAnalysis.V2
{
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    public class BaseListElement : ContainerElement
    {
        public override SyntaxToken Analyze(SyntaxToken token)
        {
            token = token.GetNextToken();
            token = base.Analyze(token);
            throw new System.NotImplementedException();
        }

        public override bool IsTokenAcceptedInContainer(SyntaxToken token)
        {
            return token.Parent.IsKind(SyntaxKind.IdentifierName);
        }

        public override bool ShouldContainerBreak(SyntaxToken token)
        {
            return false;
        }
    }
}