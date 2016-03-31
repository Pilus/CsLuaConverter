namespace CsLuaConverter.CodeTreeLuaVisitor.Expression.TwoSided
{
    using CodeTree;
    using Microsoft.CodeAnalysis.CSharp;

    public class MultiplyExpressionVisitor : TwoSidedExpressionVisitorBase
    {
        public MultiplyExpressionVisitor(CodeTreeBranch branch) : base(branch, SyntaxKind.AsteriskToken)
        {
        }
    }
}