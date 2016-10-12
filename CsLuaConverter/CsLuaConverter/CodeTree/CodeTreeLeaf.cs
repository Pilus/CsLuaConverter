namespace CsLuaConverter.CodeTree
{
    using System.Diagnostics;
    using Microsoft.CodeAnalysis;

    [DebuggerDisplay("CodeTreeLeaf - {Kind}")]
    public class CodeTreeLeaf : CodeTreeNode
    {
        public string Text;
        public SyntaxToken Token;

        public CodeTreeLeaf(SyntaxToken token)
        {
            this.Token = token;
            this.Kind = token.GetKind();
            this.Text = token.Text;
        }

        public override CodeTreeNode Clone()
        {
            return new CodeTreeLeaf(this.Token);
        }
    }
}