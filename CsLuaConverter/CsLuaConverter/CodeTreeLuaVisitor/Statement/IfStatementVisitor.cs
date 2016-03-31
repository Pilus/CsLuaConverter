namespace CsLuaConverter.CodeTreeLuaVisitor.Statement
{
    using System.CodeDom.Compiler;
    using CodeTree;
    using Providers;

    public class IfStatementVisitor : BaseVisitor
    {
        public IfStatementVisitor(CodeTreeBranch branch) : base(branch)
        {
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            throw new System.NotImplementedException();
        }
    }
}