namespace CsLuaConverter.CodeTreeLuaVisitor.Expression.Binary
{
    using CodeTree;
    using Microsoft.CodeAnalysis.CSharp;

    public class LeftShiftExpressionVisitor : BinaryExpressionAsMethodCallVisitorBase
    {
        public LeftShiftExpressionVisitor(CodeTreeBranch branch) : base(branch, SyntaxKind.LessThanLessThanToken, "bit.lshift")
        {
        }
    }
}