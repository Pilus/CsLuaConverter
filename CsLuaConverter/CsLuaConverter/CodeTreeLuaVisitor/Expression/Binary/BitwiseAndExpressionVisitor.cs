namespace CsLuaConverter.CodeTreeLuaVisitor.Expression.Binary
{
    using CodeTree;
    using Microsoft.CodeAnalysis.CSharp;

    public class BitwiseAndExpressionVisitor : BinaryExpressionAsMethodCallVisitorBase
    {
        public BitwiseAndExpressionVisitor(CodeTreeBranch branch) : base(branch, SyntaxKind.AmpersandToken, "bit.band")
        {
        }
    }
}