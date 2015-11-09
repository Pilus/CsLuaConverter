namespace CsLuaConverter.CodeElementAnalysis
{
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    public class Parameter : BaseElement
    {
        public string Name;

        public override SyntaxToken Analyze(SyntaxToken token)
        {
            ExpectKind(SyntaxKind.Parameter, token.Parent.GetKind());
            ExpectKind(SyntaxKind.IdentifierToken, token.GetKind());
            this.Name = token.Text;
            return token;
        }
    }
}