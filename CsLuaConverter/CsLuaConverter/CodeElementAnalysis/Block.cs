namespace CsLuaConverter.CodeElementAnalysis
{
    using System;
    using System.Collections.Generic;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    public class Block : BaseElement
    {
        protected IList<Line> Lines = new List<Line>();

        public override SyntaxToken Analyze(SyntaxToken token)
        {
            ExpectKind(SyntaxKind.Block, token.Parent.GetKind());
            ExpectKind(SyntaxKind.OpenBraceToken, token.GetKind());
            token = token.GetNextToken();

            while (!(token.Parent.IsKind(SyntaxKind.Block) && token.IsKind(SyntaxKind.CloseBraceToken)))
            {
                var line = new Line();

                token = line.Analyze(token);
                this.Lines.Add(line);
                token = token.GetNextToken();
            }

            return token;

        }
    }
}