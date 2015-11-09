namespace CsLuaConverter.CodeElementAnalysis
{
    using System.Collections.Generic;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    public class GenericName : BaseElement
    {
        public string Name;
        public TypeArgumentList ArgumentList;

        public override SyntaxToken Analyze(SyntaxToken token)
        {
            ExpectKind(SyntaxKind.GenericName, token.Parent.GetKind());
            ExpectKind(SyntaxKind.IdentifierToken, token.GetKind());
            this.Name = token.Text;
            token = token.GetNextToken();

            this.ArgumentList = new TypeArgumentList();

            return this.ArgumentList.Analyze(token);
        }
    }
}