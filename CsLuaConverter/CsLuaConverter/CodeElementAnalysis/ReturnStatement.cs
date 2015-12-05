namespace CsLuaConverter.CodeElementAnalysis
{
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    public class ReturnStatement : BaseElement
    {
        public override SyntaxToken Analyze(SyntaxToken token)
        {
            ExpectKind(SyntaxKind.ReturnStatement, token.Parent.GetKind());
            ExpectKind(SyntaxKind.ReturnKeyword, token.GetKind());
            return token;
        }
    }
}