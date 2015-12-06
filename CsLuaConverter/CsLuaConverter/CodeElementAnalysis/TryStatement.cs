namespace CsLuaConverter.CodeElementAnalysis
{
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    public class TryStatement : BaseElement
    {
        public Block TryBlock;
        public Block CatchBlock;
        public CatchDeclaration CatchDeclaration;
        public Block FinallyBlock;

        public override SyntaxToken Analyze(SyntaxToken token)
        {
            ExpectKind(SyntaxKind.TryStatement, token.Parent.GetKind());
            ExpectKind(SyntaxKind.TryKeyword, token.GetKind());

            token = token.GetNextToken();
            this.TryBlock = new Block();
            token = this.TryBlock.Analyze(token);

            var nextToken = token.GetNextToken();
            if (nextToken.Parent.IsKind(SyntaxKind.CatchClause))
            {
                token = nextToken.GetNextToken();
                this.CatchDeclaration = new CatchDeclaration();
                token = this.CatchDeclaration.Analyze(token);

                token = token.GetNextToken();
                this.CatchBlock = new Block();
                token = this.CatchBlock.Analyze(token);
                nextToken = token.GetNextToken();
            }

            if (nextToken.Parent.IsKind(SyntaxKind.FinallyClause))
            {
                token = nextToken.GetNextToken();
                this.FinallyBlock = new Block();
                token = this.FinallyBlock.Analyze(token);
            }

            return token;
        }
    }
}