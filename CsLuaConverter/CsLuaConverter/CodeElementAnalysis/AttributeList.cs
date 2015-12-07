namespace CsLuaConverter.CodeElementAnalysis
{
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    public class AttributeList : BaseElement
    {
        public IdentifierName IdentifierName;
        public AttributeArgumentList AttributeArgumentList;

        public override SyntaxToken Analyze(SyntaxToken token)
        {
            ExpectKind(SyntaxKind.AttributeList, token.Parent.GetKind());
            ExpectKind(SyntaxKind.OpenBracketToken, token.GetKind());

            token = token.GetNextToken();
            this.IdentifierName = new IdentifierName();
            token = this.IdentifierName.Analyze(token);

            token = token.GetNextToken();
            this.AttributeArgumentList = new AttributeArgumentList();
            token = this.AttributeArgumentList.Analyze(token);

            token = token.GetNextToken();
            ExpectKind(SyntaxKind.AttributeList, token.Parent.GetKind());
            ExpectKind(SyntaxKind.CloseBracketToken, token.GetKind());

            return token;
        }
    }
}