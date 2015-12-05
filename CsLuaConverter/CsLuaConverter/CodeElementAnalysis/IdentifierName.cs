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
            
            while (token.Parent.IsKind(SyntaxKind.IdentifierName))
            {
                this.Names.Add(token.Text);

                token = token.GetNextToken();

                if (token.Parent.IsKind(SyntaxKind.QualifiedName) && token.IsKind(SyntaxKind.DotToken))
                {
                    token = token.GetNextToken();
                }
            }
            
            return token.GetPreviousToken();
        }
    }
}