namespace CsLuaConverter.CodeElementAnalysis
{
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Statements;

    public class IfStatement : BaseElement
    {
        public Statement Statement;
        public Block Block;
        public ElseClause Else;

        public override SyntaxToken Analyze(SyntaxToken token)
        {
            ExpectKind(SyntaxKind.IfStatement, token.Parent.GetKind());
            ExpectKind(SyntaxKind.IfKeyword, token.GetKind());

            token = token.GetNextToken();
            ExpectKind(SyntaxKind.IfStatement, token.Parent.GetKind());
            ExpectKind(SyntaxKind.OpenParenToken, token.GetKind());

            token = token.GetNextToken();
            this.Statement = new Statement();
            token = this.Statement.Analyze(token);

            token = token.GetNextToken();
            this.Block = new Block();

            if (token.Parent.IsKind(SyntaxKind.Block))
            {
                token = this.Block.Analyze(token);
            }
            else
            {
                var statement = new Statement();
                token = statement.Analyze(token);
                this.Block.Statements.Add(statement);
            }

            var nextToken = token.GetNextToken();
            if (nextToken.Parent.IsKind(SyntaxKind.ElseClause))
            {
                this.Else = new ElseClause();
                token = this.Else.Analyze(nextToken);
            }

            return token;
        }
    }
}