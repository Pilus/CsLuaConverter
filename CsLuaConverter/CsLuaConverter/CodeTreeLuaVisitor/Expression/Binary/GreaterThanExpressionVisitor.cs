namespace CsLuaConverter.CodeTreeLuaVisitor.Expression.Binary
{
    using CodeTree;
    using Microsoft.CodeAnalysis.CSharp;

    public class GreaterThanExpressionVisitor : BinaryExpressionVisitorBase
    {
        public GreaterThanExpressionVisitor(CodeTreeBranch branch) : base(branch, SyntaxKind.GreaterThanToken, ">")
        {
        }
    }
}