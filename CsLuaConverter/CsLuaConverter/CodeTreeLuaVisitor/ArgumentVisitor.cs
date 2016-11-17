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

        public override void Visit(IIndentedTextWriterWrapper textWriter, IContext context)
        {
            this.inner.Visit(textWriter, context);
        }
    }
}