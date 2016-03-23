namespace CsLuaConverter.CodeTreeLuaVisitor.Type
{
    using System.CodeDom.Compiler;
    using System.Linq;
    using CodeTree;
    using Providers;

    public class PredefinedTypeVisitor : BaseTypeVisitor, ITypeVisitor
    {
        private string text;

        public PredefinedTypeVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.text = ((CodeTreeLeaf) this.Branch.Nodes.Single()).Text;
        }

        public override void Visit(IndentedTextWriter textWriter, IProviders providers)
        {
            throw new System.NotImplementedException();
        }

        public override void WriteAsReference(IndentedTextWriter textWriter, IProviders providers)
        {
            if (this.text == "void")
            {
                throw new VisitorException("Can not write void type as refrence.");
            }

            var type = providers.TypeProvider.LookupType(this.text);
            textWriter.Write("({0} % _M.DOT).typeof", type.FullNameWithoutGenerics);
        }
    }
}