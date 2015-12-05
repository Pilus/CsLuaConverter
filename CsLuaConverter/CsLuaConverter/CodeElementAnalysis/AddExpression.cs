namespace CsLuaConverter.CodeElementAnalysis
{
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    public class AddExpression : BaseElement
    {
        public override SyntaxToken Analyze(SyntaxToken token)
        {
            ExpectKind(SyntaxKind.AddExpression, token.Parent.GetKind());
            ExpectKind(SyntaxKind.PlusToken, token.GetKind());

            return token;
        }
    }
}