namespace CsLuaConverter.CodeTreeLuaVisitor
{
    using System.Linq;
    using CodeTree;
    using Providers;
    using CsLuaConverter.CodeTreeLuaVisitor.Expression.Lambda;

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

        public int? GetInputArgCountOfLambda()
        {
            var lambda = this.inner as ILambdaVisitor;
            return lambda?.GetNumParameters();
        }
    }
}