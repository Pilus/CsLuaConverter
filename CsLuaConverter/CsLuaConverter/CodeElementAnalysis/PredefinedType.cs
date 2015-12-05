namespace CsLuaConverter.CodeElementAnalysis
{
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    public class PredefinedType : BaseElement
    {
        public string Text;
        public bool IsArray;

        public override SyntaxToken Analyze(SyntaxToken token)
        {
            ExpectKind(SyntaxKind.PredefinedType, token.Parent.GetKind());
            this.Text = token.Text;

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