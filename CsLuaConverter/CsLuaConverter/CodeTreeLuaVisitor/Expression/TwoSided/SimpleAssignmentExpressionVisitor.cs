namespace CsLuaConverter.CodeTreeLuaVisitor.Expression.TwoSided
{
    using CodeTree;
    using Microsoft.CodeAnalysis.CSharp;

    public class SimpleAssignmentExpressionVisitor : TwoSidedExpressionVisitorBase
    {
        public SimpleAssignmentExpressionVisitor(CodeTreeBranch branch) : base(branch, SyntaxKind.EqualsToken)
        {
        }
    }
}