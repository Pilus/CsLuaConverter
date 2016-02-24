namespace CsLuaConverter.CodeElementAnalysis
{
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    public class UsingDirective : BaseElement
    {
        public IdentifierName Name;
         
        public override SyntaxToken Analyze(SyntaxToken token)
        {
            ExpectKind(SyntaxKind.UsingDirective, token.Parent.GetKind());
            ExpectKind(SyntaxKind.UsingKeyword, token.GetKind());
            token = token.GetNextToken();

            this.Name = new IdentifierName();
            token = this.Name.Analyze(token);
            token = token.GetNextToken();

            ExpectKind(SyntaxKind.SemicolonToken, token.GetKind());
            return token;
        }
    }
}