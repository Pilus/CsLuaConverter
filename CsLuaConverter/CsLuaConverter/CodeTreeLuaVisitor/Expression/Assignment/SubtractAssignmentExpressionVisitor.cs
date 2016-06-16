namespace CsLuaConverter.CodeTreeLuaVisitor.Expression.Assignment
{
    using CodeTree;
    using Microsoft.CodeAnalysis.CSharp;

    public class SubtractAssignmentExpressionVisitor : AssignmentExpressionVisitorBase
    {
        public SubtractAssignmentExpressionVisitor(CodeTreeBranch branch) : base(branch, SyntaxKind.MinusEqualsToken, " - ")
        {
        }
    }
}