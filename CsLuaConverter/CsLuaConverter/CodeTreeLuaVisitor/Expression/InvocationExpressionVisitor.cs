namespace CsLuaConverter.CodeTreeLuaVisitor.Expression
{
    using System.CodeDom.Compiler;
    using CodeTree;
    using Lists;
    using Providers;

    public class InvocationExpressionVisitor : BaseVisitor
    {
        private readonly BaseVisitor target;
        private readonly ArgumentListVisitor argumentList;
        public InvocationExpressionVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.target = this.CreateVisitor(0);
            this.argumentList = (ArgumentListVisitor) this.CreateVisitor(1);
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            this.target.Visit(textWriter, providers);
            this.argumentList.Visit(textWriter, providers);
        }
    }
}