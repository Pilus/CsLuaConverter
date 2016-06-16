namespace CsLuaConverter.CodeTreeLuaVisitor.Expression.Assignment
{
    using CodeTree;
    using Microsoft.CodeAnalysis.CSharp;

    public class AddAssignmentExpressionVisitor : AssignmentExpressionVisitorBase
    {
        public AddAssignmentExpressionVisitor(CodeTreeBranch branch) : base(branch, SyntaxKind.PlusEqualsToken, " +_M.Add+ ")
        {
        }
    }
}