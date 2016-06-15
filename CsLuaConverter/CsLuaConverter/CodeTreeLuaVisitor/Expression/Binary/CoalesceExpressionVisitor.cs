namespace CsLuaConverter.CodeTreeLuaVisitor.Expression.Binary
{
    using CodeTree;
    using Microsoft.CodeAnalysis.CSharp;

    public class CoalesceExpressionVisitor : BinaryExpressionVisitorBase
    {
        public CoalesceExpressionVisitor(CodeTreeBranch branch) : base(branch, SyntaxKind.QuestionQuestionToken, "or")
        {
        }
    }
}