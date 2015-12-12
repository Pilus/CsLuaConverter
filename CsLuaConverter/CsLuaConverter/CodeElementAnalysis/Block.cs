namespace CsLuaConverter.CodeElementAnalysis
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;
    using Statements;

    public class Block : BaseElement
    {
        public IList<Statement> Statements = new List<Statement>();

        public override SyntaxToken Analyze(SyntaxToken token)
        {
            ExpectKind(SyntaxKind.Block, token.Parent.GetKind());
            ExpectKind(SyntaxKind.OpenBraceToken, token.GetKind());
            var parent = (BlockSyntax) token.Parent;
            token = token.GetNextToken();

            while (!(token.Parent.IsKind(SyntaxKind.Block) && token.IsKind(SyntaxKind.CloseBraceToken)))
            {
                var statement = new Statement();

                token = statement.Analyze(token);
                this.Statements.Add(statement);

                if (token != parent.CloseBraceToken)
                {
                    token = token.GetNextToken();
                }
            }

            if (token != parent.CloseBraceToken)
            {
                throw new Exception("Expected closing token.");
            }

            return token;
        }
    }
}