namespace CsLuaConverter.CodeTreeLuaVisitor.Expression.Binary
{
    using CodeTree;
    using Microsoft.CodeAnalysis.CSharp;

    public class ExclusiveOrExpressionVisitor : BinaryExpressionAsMethodCallVisitorBase
    {
        public ExclusiveOrExpressionVisitor(CodeTreeBranch branch) : base(branch, SyntaxKind.CaretToken, "bit.bxor")
        {
        }
    }
}