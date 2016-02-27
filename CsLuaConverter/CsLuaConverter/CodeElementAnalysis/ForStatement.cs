namespace CsLuaConverter.CodeElementAnalysis
{
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Statements;

    public class ForStatement : BaseElement
    {
        public BaseElement IteratorType;
        public BaseElement IteratorName;
        public Statement StartValue;
        public Statement Condition;
        public Statement Incrementor;
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

            this.IteratorName = GenerateMatchingElement(token);
            token = this.IteratorName.Analyze(token);
            token = token.GetNextToken();

            ExpectKind(SyntaxKind.EqualsValueClause, token.Parent.GetKind());
            ExpectKind(SyntaxKind.EqualsToken, token.GetKind());
            token = token.GetNextToken();

            this.StartValue = new Statement();
            token = this.StartValue.Analyze(token);
            token = token.GetNextToken();

            this.Condition = new Statement();
            token = this.Condition.Analyze(token);
            token = token.GetNextToken();

            this.Incrementor = new Statement();
            token = this.Incrementor.Analyze(token);
            token = token.GetNextToken();

            this.Block = new Block();
            token = this.Block.Analyze(token);

            return token;
        }
    }
}