namespace CsLuaConverter.CodeTreeLuaVisitor.Expression.Binary
{
    using CodeTree;
    using Microsoft.CodeAnalysis.CSharp;
    using Providers.TypeKnowledgeRegistry;

    public class LessThanOrEqualExpressionVisitor : BinaryExpressionVisitorBase
    {
        public LessThanOrEqualExpressionVisitor(CodeTreeBranch branch) : base(branch, SyntaxKind.LessThanEqualsToken, "<=")
        {
        }
    }
}