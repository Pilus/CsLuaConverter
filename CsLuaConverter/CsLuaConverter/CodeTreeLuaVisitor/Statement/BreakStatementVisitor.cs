namespace CsLuaConverter.CodeTreeLuaVisitor.Statement
{
    using CodeTree;
    using CsLuaConverter.Context;

    public class BreakStatementVisitor : BaseVisitor
    {
        public BreakStatementVisitor(CodeTreeBranch branch) : base(branch)
        {
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IContext context)
        {
            textWriter.WriteLine("break;");
        }
    }
}