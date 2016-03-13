namespace CsLuaConverter.CodeElementAnalysis.Statements
{
    using Expressions;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    public class IfStatement : BaseStatement
    {
        public ExpressionBase Expression;
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
            this.Expression = ExpressionBase.CreateExpression(token);
            token = this.Expression.Analyze(token);

            token = token.GetNextToken();
            this.Block = new Block();

            if (token.Parent.IsKind(SyntaxKind.Block))
            {
                token = this.Block.Analyze(token);
            }
            else
            {
                var statement = CreateStatement(token);
                token = statement.Analyze(token);
                this.Block.Elements.Add(statement);
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