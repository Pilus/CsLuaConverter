namespace CsLuaConverter.CodeTreeLuaVisitor
{
    using System.Linq;
    using CodeTree;
    using CsLuaConverter.CodeTreeLuaVisitor.Expression.Lambda;
    using CsLuaConverter.Context;

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