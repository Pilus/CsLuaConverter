namespace CsLuaConverter.CodeElementAnalysis
{
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    public class SubtractAssignmentExpression : BaseElement
    {
        public override SyntaxToken Analyze(SyntaxToken token)
        {
            ExpectKind(SyntaxKind.SubtractAssignmentExpression, token.Parent.GetKind());
            ExpectKind(SyntaxKind.MinusEqualsToken, token.GetKind());

            return token;
        }
    }
}