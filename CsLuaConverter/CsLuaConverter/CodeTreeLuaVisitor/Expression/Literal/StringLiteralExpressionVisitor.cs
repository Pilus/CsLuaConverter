namespace CsLuaConverter.CodeTreeLuaVisitor.Expression.Literal
{
    using CodeTree;

    public class StringLiteralExpressionVisitor : LiteralVisitorBase
    {
        public StringLiteralExpressionVisitor(CodeTreeBranch branch) : base(branch, typeof(string))
        {
        }
    }
}