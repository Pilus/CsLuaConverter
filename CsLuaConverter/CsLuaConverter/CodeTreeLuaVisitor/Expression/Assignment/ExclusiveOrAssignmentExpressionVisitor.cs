namespace CsLuaConverter.CodeTreeLuaVisitor.Expression.Assignment
{
    using CodeTree;
    using Microsoft.CodeAnalysis.CSharp;

    public class ExclusiveOrAssignmentExpressionVisitor : AssignmentExpressionVisitorBase
    {
        public ExclusiveOrAssignmentExpressionVisitor(CodeTreeBranch branch) : base(branch, SyntaxKind.CaretEqualsToken, ", ", "bit.bxor(", ")")
        {
        }
    }
}