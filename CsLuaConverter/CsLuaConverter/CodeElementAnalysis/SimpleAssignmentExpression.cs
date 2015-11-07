namespace CsLuaConverter.CodeElementAnalysis
{
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    public class SimpleAssignmentExpression : BaseElement
    {
        public string Text;
        public override SyntaxToken Analyze(SyntaxToken token)
        {
            ExpectKind(SyntaxKind.SimpleAssignmentExpression, token.Parent.GetKind());
            this.Text = token.Text;
            return token;
        }
    }
}