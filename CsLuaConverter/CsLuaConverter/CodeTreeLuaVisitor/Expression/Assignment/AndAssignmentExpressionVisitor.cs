namespace CsLuaConverter.CodeTreeLuaVisitor.Expression.Assignment
{
    using CodeTree;
    using Microsoft.CodeAnalysis.CSharp;

    public class AndAssignmentExpressionVisitor : AssignmentExpressionVisitorBase
    {
        public AndAssignmentExpressionVisitor(CodeTreeBranch branch) : base(branch, SyntaxKind.AmpersandEqualsToken, ", ", "bit.band(", ")")
        {
        }
    }
}