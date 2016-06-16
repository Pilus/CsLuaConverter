namespace CsLuaConverter.CodeTreeLuaVisitor.Expression.Assignment
{
    using CodeTree;
    using Microsoft.CodeAnalysis.CSharp;

    public class ModuloAssignmentExpressionVisitor : AssignmentExpressionVisitorBase
    {
        public ModuloAssignmentExpressionVisitor(CodeTreeBranch branch) : base(branch, SyntaxKind.PercentEqualsToken, ", ", "math.mod(",")")
        {
        }
    }
}