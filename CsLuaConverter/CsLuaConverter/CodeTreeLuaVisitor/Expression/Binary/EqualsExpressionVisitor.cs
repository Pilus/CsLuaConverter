namespace CsLuaConverter.CodeTreeLuaVisitor.Expression.Binary
{
    using CodeTree;
    using Microsoft.CodeAnalysis.CSharp;
    using Providers.TypeKnowledgeRegistry;

    public class EqualsExpressionVisitor : BinaryExpressionVisitorBase
    {
        public EqualsExpressionVisitor(CodeTreeBranch branch) : base(branch, SyntaxKind.EqualsEqualsToken, null)
        {
        }
    }
}