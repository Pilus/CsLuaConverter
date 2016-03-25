namespace CsLuaConverter.CodeTreeLuaVisitor.Expression
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