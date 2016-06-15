namespace CsLuaConverter.CodeTreeLuaVisitor.Expression.Binary
{
    using CodeTree;
    using Microsoft.CodeAnalysis.CSharp;

    public class BitwiseOrExpressionVisitor : BinaryExpressionAsMethodCallVisitorBase
    {
        public BitwiseOrExpressionVisitor(CodeTreeBranch branch) : base(branch, SyntaxKind.BarToken, "bit.bor")
        {
        }
    }
}