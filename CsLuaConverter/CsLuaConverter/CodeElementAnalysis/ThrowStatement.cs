namespace CsLuaConverter.CodeElementAnalysis
{
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    public class ThrowStatement : BaseElement
    {
        public Statement Statement;
        public override SyntaxToken Analyze(SyntaxToken token)
        {
            ExpectKind(SyntaxKind.ThrowStatement, token.Parent.GetKind());
            ExpectKind(SyntaxKind.ThrowKeyword, token.GetKind());

            token = token.GetNextToken();
            this.Statement = new Statement();
            token = this.Statement.Analyze(token);

            ExpectKind(SyntaxKind.ThrowStatement, token.Parent.GetKind());
            ExpectKind(SyntaxKind.SemicolonToken, token.GetKind());
            return token.GetPreviousToken();
        }
    }
}