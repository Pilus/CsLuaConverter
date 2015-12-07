namespace CsLuaConverter.CodeElementAnalysis
{
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    public class Parameter : BaseElement
    {
        public bool IsParams;
        public string Name;

        public override SyntaxToken Analyze(SyntaxToken token)
        {
            ExpectKind(SyntaxKind.Parameter, token.Parent.GetKind());

            if (token.IsKind(SyntaxKind.ParamsKeyword))
            {
                this.IsParams = true;
                return token;
            }

            ExpectKind(SyntaxKind.IdentifierToken, token.GetKind());
            this.Name = token.Text;
            return token;
        }
    }
}