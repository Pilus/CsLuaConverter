namespace CsLuaConverter.CodeTreeLuaVisitor.Expression.Binary
{
    using CodeTree;
    using Microsoft.CodeAnalysis.CSharp;

    public class LogicalAndExpressionVisitor : BinaryExpressionVisitorBase
    {
        public LogicalAndExpressionVisitor(CodeTreeBranch branch) : base(branch, SyntaxKind.AmpersandAmpersandToken, "and")
        {
        }
    }
}