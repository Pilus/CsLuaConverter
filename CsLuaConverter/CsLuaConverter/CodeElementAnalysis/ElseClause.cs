namespace CsLuaConverter.CodeElementAnalysis
{
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    public class ElseClause : BaseElement
    {
        public Statement Statement;

        public override SyntaxToken Analyze(SyntaxToken token)
        {
            ExpectKind(SyntaxKind.ElseClause, token.Parent.GetKind());
            ExpectKind(SyntaxKind.ElseKeyword, token.GetKind());

            token = token.GetNextToken();
            this.Statement = new Statement();
            return this.Statement.Analyze(token);
        }
    }
}