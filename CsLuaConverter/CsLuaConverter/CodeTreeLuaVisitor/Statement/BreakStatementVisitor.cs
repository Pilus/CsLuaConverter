namespace CsLuaConverter.CodeTreeLuaVisitor.Statement
{
    using CodeTree;
    using Providers;

    public class BreakStatementVisitor : BaseVisitor
    {
        public BreakStatementVisitor(CodeTreeBranch branch) : base(branch)
        {
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            textWriter.WriteLine("break;");
            providers.Context.CurrentType = null;
        }
    }
}