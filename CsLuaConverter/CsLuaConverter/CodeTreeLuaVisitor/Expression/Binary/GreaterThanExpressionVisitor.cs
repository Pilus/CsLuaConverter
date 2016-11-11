namespace CsLuaConverter.CodeTreeLuaVisitor.Expression.Binary
{
    using CodeTree;
    using Microsoft.CodeAnalysis.CSharp;
    using Providers.TypeKnowledgeRegistry;

    public class GreaterThanExpressionVisitor : BinaryExpressionVisitorBase
    {
        public GreaterThanExpressionVisitor(CodeTreeBranch branch) : base(branch, SyntaxKind.GreaterThanToken, ">")
        {
        }
    }
}