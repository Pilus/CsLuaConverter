namespace CsLuaConverter.CodeTreeLuaVisitor.Expression.Binary
{
    using CodeTree;
    using Microsoft.CodeAnalysis.CSharp;

    public class LessThanOrEqualExpressionVisitor : BinaryExpressionVisitorBase
    {
        public LessThanOrEqualExpressionVisitor(CodeTreeBranch branch) : base(branch, SyntaxKind.LessThanEqualsToken, "<=")
        {
        }
    }
}