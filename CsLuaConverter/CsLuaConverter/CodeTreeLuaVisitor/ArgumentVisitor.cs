namespace CsLuaConverter.CodeTreeLuaVisitor
{
    using System.Linq;
    using CodeTree;
    using Providers;
    using CsLuaConverter.CodeTreeLuaVisitor.Expression.Lambda;
    using Providers.TypeKnowledgeRegistry;

    public class ArgumentVisitor : BaseVisitor
    {
        private readonly BaseVisitor inner;
        public ArgumentVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.inner = this.CreateVisitors().Single();
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            providers.TypeKnowledgeRegistry.CurrentType = null;
            this.inner.Visit(textWriter, providers);
        }

        public bool IsArgumentVisitorALambda()
        {
            return this.inner is ILambdaVisitor;
        }

        public bool IsArgumentVisitorParenLambda()
        {
            return this.inner is ParenthesizedLambdaExpressionVisitor;
        }

        public TypeKnowledge GetReturnTypeOfSimpleLambdaVisitor(IProviders providers, TypeKnowledge inputType)
        {
            return (this.inner as SimpleLambdaExpressionVisitor)?.GetReturnType(providers, inputType);
        }

        public int? GetInputArgCountOfLambda()
        {
            var lambda = this.inner as ILambdaVisitor;
            return lambda?.GetNumParameters();
        }
    }
}