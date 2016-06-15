namespace CsLuaConverter.CodeTreeLuaVisitor.Expression.Binary
{
    using CodeTree;
    using Microsoft.CodeAnalysis.CSharp;
    using Providers.TypeKnowledgeRegistry;

    public class GreaterThanOrEqualExpressionVisitor : BinaryExpressionVisitorBase
    {
        public GreaterThanOrEqualExpressionVisitor(CodeTreeBranch branch) : base(branch, SyntaxKind.GreaterThanEqualsToken, ">=", new TypeKnowledge(typeof(bool)))
        {
        }
    }
}