namespace CsLuaConverter.CodeTreeLuaVisitor.Expression.Binary
{
    using CodeTree;
    using Microsoft.CodeAnalysis.CSharp;
    using Providers.TypeKnowledgeRegistry;

    public class LogicalAndExpressionVisitor : BinaryExpressionVisitorBase
    {
        public LogicalAndExpressionVisitor(CodeTreeBranch branch) : base(branch, SyntaxKind.AmpersandAmpersandToken, "and", new TypeKnowledge(typeof(bool)))
        {
        }
    }
}