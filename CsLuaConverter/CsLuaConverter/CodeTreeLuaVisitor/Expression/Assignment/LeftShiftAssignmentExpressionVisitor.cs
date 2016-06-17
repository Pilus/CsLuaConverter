namespace CsLuaConverter.CodeTreeLuaVisitor.Expression.Assignment
{
    using CodeTree;
    using Microsoft.CodeAnalysis.CSharp;

    public class LeftShiftAssignmentExpressionVisitor : AssignmentExpressionVisitorBase
    {
        public LeftShiftAssignmentExpressionVisitor(CodeTreeBranch branch) : base(branch, SyntaxKind.LessThanLessThanEqualsToken, ", ", "bit.lshift(", ")")
        {
        }
    }
}