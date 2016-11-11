namespace CsLuaConverter.CodeTreeLuaVisitor.Expression.Binary
{
    using CodeTree;
    using Microsoft.CodeAnalysis.CSharp;
    using Providers.TypeKnowledgeRegistry;

    public class LessThanExpressionVisitor : BinaryExpressionVisitorBase
    {
        public LessThanExpressionVisitor(CodeTreeBranch branch) : base(branch, SyntaxKind.LessThanToken, "<")
        {
        }
    }
}