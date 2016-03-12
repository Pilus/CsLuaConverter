namespace CsLuaConverter.CodeElementAnalysis.Statements
{
    using System.Collections.Generic;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    public class TryStatement : BaseStatement
    {
        public Block TryBlock;
        public Block FinallyBlock;
        public IEnumerable<CatchPair> CatchPairs;

        public override SyntaxToken Analyze(SyntaxToken token)
        {
            ExpectKind(SyntaxKind.TryStatement, token.Parent.GetKind());
            ExpectKind(SyntaxKind.TryKeyword, token.GetKind());

            token = token.GetNextToken();
            this.TryBlock = new Block();
            token = this.TryBlock.Analyze(token);

            var nextToken = token.GetNextToken();

            var catchPairs = new List<CatchPair>();

            while (nextToken.Parent.IsKind(SyntaxKind.CatchClause))
            {
                token = nextToken.GetNextToken();

                var pair = new CatchPair();

                if (token.Parent.IsKind(SyntaxKind.CatchDeclaration))
                {
                    pair.Declaration = new CatchDeclaration();
                    token = pair.Declaration.Analyze(token);
                    token = token.GetNextToken();
                }

                pair.Block = new Block();
                token = pair.Block.Analyze(token);
                nextToken = token.GetNextToken();

                catchPairs.Add(pair);
            }

            this.CatchPairs = catchPairs;

            if (nextToken.Parent.IsKind(SyntaxKind.FinallyClause))
            {
                token = nextToken.GetNextToken();
                this.FinallyBlock = new Block();
                token = this.FinallyBlock.Analyze(token);
            }

            return token;
        }
    }

    public struct CatchPair
    {
        public CatchDeclaration Declaration;
        public Block Block;

    }
}