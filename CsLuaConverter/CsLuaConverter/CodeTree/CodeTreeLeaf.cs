namespace CsLuaConverter.CodeTree
{
    using System.Diagnostics;
    using Microsoft.CodeAnalysis;

    [DebuggerDisplay("CodeTreeLeaf - {Kind}")]
    public class CodeTreeLeaf : CodeTreeNode
    {
        public string Text;

        public CodeTreeLeaf(SyntaxToken token)
        {
            this.Kind = token.GetKind();
            this.Text = token.Text;
        }
    }
}