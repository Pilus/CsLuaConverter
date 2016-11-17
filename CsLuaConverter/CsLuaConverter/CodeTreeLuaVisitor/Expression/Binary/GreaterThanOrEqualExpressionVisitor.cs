namespace CsLuaConverter.CodeTreeLuaVisitor.Expression.Binary
{
    using CodeTree;
    using Microsoft.CodeAnalysis.CSharp;

    public class GreaterThanOrEqualExpressionVisitor : BinaryExpressionVisitorBase
    {
        public GreaterThanOrEqualExpressionVisitor(CodeTreeBranch branch) : base(branch, SyntaxKind.GreaterThanEqualsToken, ">=")
        {
        }
    }
}