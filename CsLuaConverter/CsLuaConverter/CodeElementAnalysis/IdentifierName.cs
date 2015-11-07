namespace CsLuaConverter.CodeElementAnalysis
{
    using System.Collections.Generic;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    public class IdentifierName : BaseElement
    {
        public readonly List<string> Names = new List<string>();

        public override SyntaxToken Analyze(SyntaxToken token)
        {
            ExpectKind(SyntaxKind.IdentifierName, token.Parent.GetKind());
            this.Names.Add(token.Text);

            while (token.GetNextToken().Parent.IsKind(SyntaxKind.IdentifierName))
            {
                token = token.GetNextToken();
                this.Names.Add(token.Text);
            }
            
            return token;
        }
    }
}