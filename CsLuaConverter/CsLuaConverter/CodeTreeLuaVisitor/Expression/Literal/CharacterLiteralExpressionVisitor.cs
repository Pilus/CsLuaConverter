namespace CsLuaConverter.CodeTreeLuaVisitor.Expression.Literal
{
    using CodeTree;

    public class CharacterLiteralExpressionVisitor : LiteralVisitorBase
    {
        public CharacterLiteralExpressionVisitor(CodeTreeBranch branch) : base(branch, typeof(string))
        {
        }
    }
}