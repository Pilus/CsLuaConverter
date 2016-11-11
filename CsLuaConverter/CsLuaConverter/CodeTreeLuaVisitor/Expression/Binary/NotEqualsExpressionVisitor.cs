namespace CsLuaConverter.CodeTreeLuaVisitor.Expression.Binary
{
    using CodeTree;
    using Microsoft.CodeAnalysis.CSharp;
    using Providers.TypeKnowledgeRegistry;

    public class NotEqualsExpressionVisitor : BinaryExpressionVisitorBase
    {
        public NotEqualsExpressionVisitor(CodeTreeBranch branch) : base(branch, SyntaxKind.ExclamationEqualsToken, "~=")
        {
        }
    }
}