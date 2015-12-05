namespace CsLuaConverter.CodeElementAnalysis
{
    using System.Collections.Generic;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    public class GenericName : BaseElement
    {
        public string Name;
        public TypeArgumentList ArgumentList;
        public bool IsArray;

        public override SyntaxToken Analyze(SyntaxToken token)
        {
            ExpectKind(SyntaxKind.GenericName, token.Parent.GetKind());
            ExpectKind(SyntaxKind.IdentifierToken, token.GetKind());
            this.Name = token.Text;
            token = token.GetNextToken();

            this.ArgumentList = new TypeArgumentList();
            token = this.ArgumentList.Analyze(token);

            if (token.GetNextToken().Parent.IsKind(SyntaxKind.ArrayRankSpecifier))
            {
                token = token.GetNextToken();
                ExpectKind(SyntaxKind.OpenBracketToken, token.GetKind());
                token = token.GetNextToken();
                ExpectKind(SyntaxKind.ArrayRankSpecifier, token.Parent.GetKind());
                ExpectKind(SyntaxKind.CloseBracketToken, token.GetKind());
                this.IsArray = true;
            }

            return token;
        }
    }
}