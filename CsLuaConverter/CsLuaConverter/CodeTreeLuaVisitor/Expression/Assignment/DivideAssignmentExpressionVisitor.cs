namespace CsLuaConverter.CodeTreeLuaVisitor.Expression.Assignment
{
    using CodeTree;
    using Microsoft.CodeAnalysis.CSharp;

    public class DivideAssignmentExpressionVisitor : AssignmentExpressionVisitorBase
    {
        public DivideAssignmentExpressionVisitor(CodeTreeBranch branch) : base(branch, SyntaxKind.SlashEqualsToken, " / ")
        {
        }
    }
}