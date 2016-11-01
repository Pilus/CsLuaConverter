namespace CsLuaConverter.CodeTreeLuaVisitor.Expression.Lambda
{
    using CodeTree;
    using Lists;
    using Microsoft.CodeAnalysis;
    using Providers;
    using Providers.TypeKnowledgeRegistry;
    using Microsoft.CodeAnalysis.CSharp;

    public class ParenthesizedLambdaExpressionVisitor : BaseLambdaExpressionVisitor
    {
        public ParenthesizedLambdaExpressionVisitor(CodeTreeBranch branch) : base(branch)
        {
        }
    }
}