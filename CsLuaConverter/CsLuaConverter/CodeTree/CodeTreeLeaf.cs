namespace CsLuaConverter.CodeTree
{
    using System.Diagnostics;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    [DebuggerDisplay("CodeTreeLeaf - {Kind}")]
    public class CodeTreeLeaf : CodeTreeNode
    {
        public string Text;

        public CodeTreeLeaf(SyntaxToken token)
        {
            this.Kind = token.GetKind();
            this.Text = token.Text;
        }

        public CodeTreeLeaf(SyntaxKind kind, string text)
        {
            this.Kind = kind;
            this.Text = text;
        }

        public override CodeTreeNode Clone()
        {
            return new CodeTreeLeaf(this.Kind, this.Text);
        }
    }
}