namespace CsLuaConverter.CodeTreeLuaVisitor.Statement
{
    using CodeTree;
    using Providers;

    public class EmptyStatementVisitor : BaseVisitor
    {
        public EmptyStatementVisitor(CodeTreeBranch branch) : base(branch)
        {
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IContext context)
        {
        }
    }
}