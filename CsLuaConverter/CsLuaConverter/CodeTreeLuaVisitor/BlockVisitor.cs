namespace CsLuaConverter.CodeTreeLuaVisitor
{
    using System.CodeDom.Compiler;
    using CodeTree;
    using Providers;

    public class BlockVisitor : BaseVisitor
    {
        public BlockVisitor(CodeTreeBranch branch) : base(branch)
        {
        }

        public override void Visit(IndentedTextWriter textWriter, IProviders providers)
        {
            //throw new System.NotImplementedException();
        }
    }
}