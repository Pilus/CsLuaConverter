namespace CsLuaConverter.CodeTreeLuaVisitor.Expression.Binary
{
    using CodeTree;
    using Microsoft.CodeAnalysis.CSharp;

    public class ModuloExpressionVisitor : BinaryExpressionAsMethodCallVisitorBase
    {
        public ModuloExpressionVisitor(CodeTreeBranch branch) : base(branch, SyntaxKind.PercentToken, "math.mod")
        {
        }
    }
}