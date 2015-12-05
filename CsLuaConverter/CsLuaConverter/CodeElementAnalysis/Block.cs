namespace CsLuaConverter.CodeElementAnalysis
{
    using System;
    using System.Collections.Generic;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    public class Block : BaseElement
    {
        protected IList<Statement> Statements = new List<Statement>();

        public override SyntaxToken Analyze(SyntaxToken token)
        {
            ExpectKind(SyntaxKind.Block, token.Parent.GetKind());
            ExpectKind(SyntaxKind.OpenBraceToken, token.GetKind());
            token = token.GetNextToken();

            while (!(token.Parent.IsKind(SyntaxKind.Block) && token.IsKind(SyntaxKind.CloseBraceToken)))
            {
                var statement = new Statement();

                token = statement.Analyze(token);
                this.Statements.Add(statement);
                token = token.GetNextToken();
            }

            return token;

        }
    }
}