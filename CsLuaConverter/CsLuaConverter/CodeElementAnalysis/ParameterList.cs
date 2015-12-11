namespace CsLuaConverter.CodeElementAnalysis
{
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    public class ParameterList : DelimiteredContainerElement
    {
        public override SyntaxToken Analyze(SyntaxToken token)
        {
            ExpectKind(SyntaxKind.ParameterList, token.Parent.GetKind());
            ExpectKind(SyntaxKind.OpenParenToken, token.GetKind());
            token = token.GetNextToken();

            return base.Analyze(token);
        }

        public override bool IsTokenAcceptedInContainer(SyntaxToken token)
        {
            return token.Parent.IsKind(SyntaxKind.PredefinedType) ||
                   token.Parent.IsKind(SyntaxKind.Parameter) ||
                   token.Parent.IsKind(SyntaxKind.IdentifierName) ||
                   token.Parent.IsKind(SyntaxKind.GenericName);
        }

        public override bool ShouldContainerBreak(SyntaxToken token)
        {
            return token.Parent.IsKind(SyntaxKind.ParameterList) && token.IsKind(SyntaxKind.CloseParenToken);
        }

        public override bool IsDelimiter(SyntaxToken token)
        {
            return token.Parent.IsKind(SyntaxKind.ParameterList) && token.IsKind(SyntaxKind.CommaToken);
        }
    }
}