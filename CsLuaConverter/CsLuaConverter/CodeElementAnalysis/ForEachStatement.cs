namespace CsLuaConverter.CodeElementAnalysis
{
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Statements;

    public class ForEachStatement : BaseElement
    {
        public BaseElement IteratorType;
        public string IteratorName;
        public Statement EnumeratorStatement;
        public Block Block;

        public override SyntaxToken Analyze(SyntaxToken token)
        {
            ExpectKind(SyntaxKind.ForEachStatement, token.Parent.GetKind());
            ExpectKind(SyntaxKind.ForEachKeyword, token.GetKind());
            
            token = token.GetNextToken();
            ExpectKind(SyntaxKind.ForEachStatement, token.Parent.GetKind());
            ExpectKind(SyntaxKind.OpenParenToken, token.GetKind());

            token = token.GetNextToken();
            if (token.Parent.IsKind(SyntaxKind.IdentifierName))
            {
                this.IteratorType = new IdentifierName();
                token = this.IteratorType.Analyze(token);
            }
            else if (token.Parent.IsKind(SyntaxKind.PredefinedType))
            {
                this.IteratorType = new PredefinedType();
                token = this.IteratorType.Analyze(token);
            }
            
            token = token.GetNextToken();
            ExpectKind(SyntaxKind.ForEachStatement, token.Parent.GetKind());
            ExpectKind(SyntaxKind.IdentifierToken, token.GetKind());
            this.IteratorName = token.Text;

            token = token.GetNextToken();
            ExpectKind(SyntaxKind.ForEachStatement, token.Parent.GetKind());
            ExpectKind(SyntaxKind.InKeyword, token.GetKind());

            token = token.GetNextToken();
            this.EnumeratorStatement = new Statement();
            token = this.EnumeratorStatement.Analyze(token);

            token = token.GetNextToken();
            this.Block = new Block();
            token = this.Block.Analyze(token);

            return token;
        }
    }
}