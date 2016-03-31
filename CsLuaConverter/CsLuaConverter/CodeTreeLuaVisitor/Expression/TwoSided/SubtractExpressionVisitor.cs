namespace CsLuaConverter.CodeTreeLuaVisitor.Expression.TwoSided
{
    using CodeTree;
    using Microsoft.CodeAnalysis.CSharp;

    public class SubtractExpressionVisitor : TwoSidedExpressionVisitorBase
    {
        public SubtractExpressionVisitor(CodeTreeBranch branch) : base(branch, SyntaxKind.MinusToken)
        {
        }
    }
}