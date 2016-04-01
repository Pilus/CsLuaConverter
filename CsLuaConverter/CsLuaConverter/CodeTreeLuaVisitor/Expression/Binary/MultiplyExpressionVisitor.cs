namespace CsLuaConverter.CodeTreeLuaVisitor.Expression.Binary
{
    using CodeTree;
    using Microsoft.CodeAnalysis.CSharp;

    public class MultiplyExpressionVisitor : BinaryExpressionVisitorBase
    {
        public MultiplyExpressionVisitor(CodeTreeBranch branch) : base(branch, SyntaxKind.AsteriskToken)
        {
        }
    }
}