namespace CsLuaConverter.CodeTreeLuaVisitor.Expression.Binary
{
    using CodeTree;
    using Microsoft.CodeAnalysis.CSharp;

    public class RightShiftExpressionVisitor : BinaryExpressionAsMethodCallVisitorBase
    {
        public RightShiftExpressionVisitor(CodeTreeBranch branch) : base(branch, SyntaxKind.GreaterThanGreaterThanToken, "bit.rshift")
        {
        }
    }
}