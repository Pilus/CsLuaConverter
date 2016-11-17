namespace CsLuaConverter.CodeTreeLuaVisitor.Expression.Literal
{
    using System;
    using CodeTree;
    using Providers;

    public class LiteralVisitorBase : BaseVisitor
    {
        private readonly string text;

        protected LiteralVisitorBase(CodeTreeBranch branch, string resultingText = null) : base(branch)
        {
            this.text = resultingText ?? ((CodeTreeLeaf)this.Branch.Nodes[0]).Text;
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IContext context)
        {
            textWriter.Write(this.text);
        }
    }
}