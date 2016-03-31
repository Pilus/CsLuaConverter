namespace CsLuaConverter.CodeTreeLuaVisitor.Statement
{
    using System.CodeDom.Compiler;
    using CodeTree;
    using Providers;

    public class ForEachStatementVisitor : BaseVisitor
    {
        public ForEachStatementVisitor(CodeTreeBranch branch) : base(branch)
        {
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            throw new System.NotImplementedException();
        }
    }
}