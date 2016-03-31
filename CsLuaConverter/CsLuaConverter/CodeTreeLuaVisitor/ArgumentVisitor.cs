namespace CsLuaConverter.CodeTreeLuaVisitor
{
    using System.CodeDom.Compiler;
    using System.Linq;
    using CodeTree;
    using Providers;

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
    }
}