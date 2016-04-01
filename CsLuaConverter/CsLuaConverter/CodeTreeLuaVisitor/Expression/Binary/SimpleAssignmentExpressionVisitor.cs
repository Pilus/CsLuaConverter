namespace CsLuaConverter.CodeTreeLuaVisitor.Expression.Binary
{
    using CodeTree;
    using Microsoft.CodeAnalysis.CSharp;

    public class SimpleAssignmentExpressionVisitor : BinaryExpressionVisitorBase
    {
        public SimpleAssignmentExpressionVisitor(CodeTreeBranch branch) : base(branch, SyntaxKind.EqualsToken)
        {
        }
    }
}