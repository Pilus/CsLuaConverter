namespace CsLuaConverter.CodeTreeLuaVisitor.Expression.Assignment
{
    using CodeTree;
    using Microsoft.CodeAnalysis.CSharp;

    public class OrAssignmentExpressionVisitor : AssignmentExpressionVisitorBase
    {
        public OrAssignmentExpressionVisitor(CodeTreeBranch branch) : base(branch, SyntaxKind.BarEqualsToken, ", ", "bit.bor(", ")")
        {
        }
    }
}