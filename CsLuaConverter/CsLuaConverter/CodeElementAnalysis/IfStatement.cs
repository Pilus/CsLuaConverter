namespace CsLuaConverter.CodeElementAnalysis
{
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    public class IfStatement : BaseElement
    {
        public Statement Statement;

        public override SyntaxToken Analyze(SyntaxToken token)
        {
            ExpectKind(SyntaxKind.IfStatement, token.Parent.GetKind());
            ExpectKind(SyntaxKind.IfKeyword, token.GetKind());

            token = token.GetNextToken();
            ExpectKind(SyntaxKind.IfStatement, token.Parent.GetKind());
            ExpectKind(SyntaxKind.OpenParenToken, token.GetKind());

            token = token.GetNextToken();
            this.Statement = new Statement();
            return this.Statement.Analyze(token);
        }
    }
}