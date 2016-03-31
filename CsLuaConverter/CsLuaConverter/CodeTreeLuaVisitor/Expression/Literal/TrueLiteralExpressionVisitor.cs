namespace CsLuaConverter.CodeTreeLuaVisitor.Expression.Literal
{
    using CodeTree;

    public class TrueLiteralExpressionVisitor : LiteralVisitorBase
    {
        public TrueLiteralExpressionVisitor(CodeTreeBranch branch) : base(branch, typeof(bool))
        {
        }
    }
}