namespace CsLuaConverter.CodeTreeLuaVisitor.Expression
{
    using System.CodeDom.Compiler;
    using CodeTree;
    using Microsoft.CodeAnalysis.CSharp;
    using Providers;

    public class SubtractExpressionVisitor : TwoSidedExpressionVisitorBase
    {
        public SubtractExpressionVisitor(CodeTreeBranch branch) : base(branch, SyntaxKind.MinusToken)
        {
        }
    }
}