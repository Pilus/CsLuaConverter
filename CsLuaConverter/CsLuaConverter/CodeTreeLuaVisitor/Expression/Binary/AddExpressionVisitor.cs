namespace CsLuaConverter.CodeTreeLuaVisitor.Expression.Binary
{
    using CodeTree;
    using Microsoft.CodeAnalysis.CSharp;

    public class AddExpressionVisitor : BinaryExpressionVisitorBase
    {
        public AddExpressionVisitor(CodeTreeBranch branch) : base(branch, SyntaxKind.PlusToken, " +_M.Add+ ")
        {
        }
    }
}