namespace CsLuaConverter.CodeElementAnalysis.Statements
{
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    public class WhileStatement : BaseStatement
    {
        public Expression Expression;
        public Block Block;

        public override SyntaxToken Analyze(SyntaxToken token)
        {
            ExpectKind(SyntaxKind.WhileStatement, token.Parent.GetKind());
            ExpectKind(SyntaxKind.WhileKeyword, token.GetKind());

            token = token.GetNextToken();
            ExpectKind(SyntaxKind.WhileStatement, token.Parent.GetKind());
            ExpectKind(SyntaxKind.OpenParenToken, token.GetKind());

            token = token.GetNextToken();
            this.Expression = new Expression();
            token = this.Expression.Analyze(token);

            token = token.GetNextToken();
            this.Block = new Block();
            token = this.Block.Analyze(token);

            return token;
        }
    }
}