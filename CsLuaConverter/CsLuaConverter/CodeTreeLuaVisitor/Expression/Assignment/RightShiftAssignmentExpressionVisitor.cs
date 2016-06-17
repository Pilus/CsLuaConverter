namespace CsLuaConverter.CodeTreeLuaVisitor.Expression.Assignment
{
    using CodeTree;
    using Microsoft.CodeAnalysis.CSharp;

    public class RightShiftAssignmentExpressionVisitor : AssignmentExpressionVisitorBase
    {
        public RightShiftAssignmentExpressionVisitor(CodeTreeBranch branch) : base(branch, SyntaxKind.GreaterThanGreaterThanEqualsToken, ", ", "bit.rshift(", ")")
        {
        }
    }
}