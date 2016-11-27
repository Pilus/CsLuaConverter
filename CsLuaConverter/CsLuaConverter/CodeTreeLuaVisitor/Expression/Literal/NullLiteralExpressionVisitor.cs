namespace CsLuaConverter.CodeTreeLuaVisitor.Expression.Literal
{
    using CodeTree;

    public class NullLiteralExpressionVisitor : LiteralVisitorBase
    {
        public NullLiteralExpressionVisitor(CodeTreeBranch branch) : base(branch, "nil")
        {
        }
    }
}