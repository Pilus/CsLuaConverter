namespace CsLuaConverter.CodeTreeLuaVisitor.Expression.Literal
{
    using System;
    using CodeTree;

    public class NullLiteralExpressionVisitor : LiteralVisitorBase
    {
        public NullLiteralExpressionVisitor(CodeTreeBranch branch) : base(branch, typeof(Nullable), "nil")
        {
        }
    }
}