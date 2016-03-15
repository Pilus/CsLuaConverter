namespace CsLuaConverter.CodeTreeLuaVisitor.Name
{
    using System.CodeDom.Compiler;
    using System.Linq;
    using CodeTree;
    using Providers;

    public class IdentifierNameVisitor : BaseVisitor, INameVisitor
    {
        public IdentifierNameVisitor(CodeTreeBranch branch) : base(branch)
        {
        }

        public override void Visit(IndentedTextWriter textWriter, IProviders providers)
        {
            throw new System.NotImplementedException();
        }

        public string[] GetName()
        {
            return new[] {((CodeTreeLeaf)this.Branch.Nodes.Single()).Text};
        }
    }
}