namespace CsLuaConverter.CodeTreeLuaVisitor.Expression.Assignment
{
    using CodeTree;
    using Microsoft.CodeAnalysis.CSharp;

    public class MultiplyAssignmentExpressionVisitor : AssignmentExpressionVisitorBase
    {
        public MultiplyAssignmentExpressionVisitor(CodeTreeBranch branch) : base(branch, SyntaxKind.AsteriskEqualsToken, " * ")
        {
        }
    }
}