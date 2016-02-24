namespace CsLuaConverter.CodeElementAnalysis
{
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    public class VariableDeclarator : BaseElement
    {
        public string Name;
        public override SyntaxToken Analyze(SyntaxToken token)
        {
            ExpectKind(SyntaxKind.VariableDeclarator, token.Parent.GetKind());
            this.Name = token.Text;
            return token;
        }
    }
}