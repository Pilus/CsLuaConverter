namespace CsLuaConverter.CodeElementAnalysis
{
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    public class AddAssignmentExpression : BaseElement
    {
        public override SyntaxToken Analyze(SyntaxToken token)
        {
            ExpectKind(SyntaxKind.AddAssignmentExpression, token.Parent.GetKind());
            ExpectKind(SyntaxKind.PlusEqualsToken, token.GetKind());

            return token;
        }
    }
}