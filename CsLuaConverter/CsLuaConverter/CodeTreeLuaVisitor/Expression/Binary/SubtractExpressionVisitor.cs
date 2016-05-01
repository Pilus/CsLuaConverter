namespace CsLuaConverter.CodeTreeLuaVisitor.Expression.Binary
{
    using CodeTree;
    using Microsoft.CodeAnalysis.CSharp;

    public class SubtractExpressionVisitor : BinaryExpressionVisitorBase
    {
        public SubtractExpressionVisitor(CodeTreeBranch branch) : base(branch, SyntaxKind.MinusToken)
        {
        }
    }
}