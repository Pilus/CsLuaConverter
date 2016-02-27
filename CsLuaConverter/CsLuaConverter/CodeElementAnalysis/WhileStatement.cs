namespace CsLuaConverter.CodeElementAnalysis
{
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Statements;

    public class WhileStatement : BaseElement
    {
        public Statement Statement;
        public Block Block;

        public override SyntaxToken Analyze(SyntaxToken token)
        {
            ExpectKind(SyntaxKind.WhileStatement, token.Parent.GetKind());
            ExpectKind(SyntaxKind.WhileKeyword, token.GetKind());

            token = token.GetNextToken();
            ExpectKind(SyntaxKind.WhileStatement, token.Parent.GetKind());
            ExpectKind(SyntaxKind.OpenParenToken, token.GetKind());

            token = token.GetNextToken();
            this.Statement = new Statement();
            token = this.Statement.Analyze(token);

            token = token.GetNextToken();
            this.Block = new Block();
            token = this.Block.Analyze(token);

            return token;
        }
    }
}