namespace CsLuaConverter.CodeTreeLuaVisitor.Expression.Binary
{
    using CodeTree;
    using Microsoft.CodeAnalysis.CSharp;

    public class DivideExpressionVisitor : BinaryExpressionVisitorBase
    {
        public DivideExpressionVisitor(CodeTreeBranch branch) : base(branch, SyntaxKind.SlashToken)
        {
        }
    }
}