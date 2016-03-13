namespace CsLuaConverter.CodeElementAnalysis.Statements
{
    using Expressions;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    public class ForStatement : BaseStatement
    {
        public BaseElement IteratorType;
        public VariableDeclarator IteratorName;
        public BaseStatement StartValue;
        public ExpressionBase Condition;
        public BaseStatement Incrementor;
        public Block Block;

        public override SyntaxToken Analyze(SyntaxToken token)
        {
            ExpectKind(SyntaxKind.ForStatement, token.Parent.GetKind());
            ExpectKind(SyntaxKind.ForKeyword, token.GetKind());
            token = token.GetNextToken();

            ExpectKind(SyntaxKind.ForStatement, token.Parent.GetKind());
            ExpectKind(SyntaxKind.OpenParenToken, token.GetKind());
            token = token.GetNextToken();

            this.IteratorType = GenerateMatchingElement(token);
            token = this.IteratorType.Analyze(token);
            token = token.GetNextToken();

            this.IteratorName = new VariableDeclarator();
            token = this.IteratorName.Analyze(token);
            token = token.GetNextToken();

            ExpectKind(SyntaxKind.EqualsValueClause, token.Parent.GetKind());
            ExpectKind(SyntaxKind.EqualsToken, token.GetKind());
            token = token.GetNextToken();

            this.StartValue = CreateStatement(token);
            token = this.StartValue.Analyze(token);
            token = token.GetNextToken();

            this.Condition = ExpressionBase.CreateExpression(token);
            token = this.Condition.Analyze(token);
            token = token.GetNextToken();

            this.Incrementor = CreateStatement(token);
            token = this.Incrementor.Analyze(token);
            token = token.GetNextToken();

            this.Block = new Block();
            token = this.Block.Analyze(token);

            return token;
        }
    }
}