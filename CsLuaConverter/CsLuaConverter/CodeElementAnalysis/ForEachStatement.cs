namespace CsLuaConverter.CodeElementAnalysis
{
    using System;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    public class ForEachStatement : BaseElement
    {
        public IdentifierName IteratorType;
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
            ExpectKind(SyntaxKind.IdentifierName, token.Parent.GetKind());
            this.IteratorType = new IdentifierName();
            token = this.IteratorType.Analyze(token);

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