namespace CsLuaConverter.CodeTree
{
    using Microsoft.CodeAnalysis.CSharp;

    public abstract class CodeTreeNode
    {
        public SyntaxKind Kind;
        public abstract CodeTreeNode Clone();
    }
}