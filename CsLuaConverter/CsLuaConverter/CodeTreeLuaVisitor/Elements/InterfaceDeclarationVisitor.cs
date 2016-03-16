namespace CsLuaConverter.CodeTreeLuaVisitor.Elements
{
    using System.CodeDom.Compiler;
    using System.Linq;
    using CodeTree;
    using Filters;
    using Lists;
    using Microsoft.CodeAnalysis.CSharp;
    using Providers;

    public class InterfaceDeclarationVisitor : BaseVisitor, IElementVisitor
    {
        private string name;
        private IListVisitor genericsVisitor;

        public InterfaceDeclarationVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.CreateNameVisitor();
            this.CreateGenericsVisitor();
        }

        public override void Visit(IndentedTextWriter textWriter, IProviders providers)
        {
            //throw new System.NotImplementedException();
        }

        public string GetName()
        {
            return this.name;
        }

        public int GetNumOfGenerics()
        {
            return this.genericsVisitor?.GetNumElements() ?? 0;
        }

        private void CreateNameVisitor()
        {
            var accessorNodes = this.GetFilteredNodes(new KindRangeFilter(null, SyntaxKind.InterfaceKeyword));
            this.ExpectKind(accessorNodes.Length, SyntaxKind.InterfaceKeyword);
            this.ExpectKind(accessorNodes.Length + 1, SyntaxKind.IdentifierToken);
            this.name = ((CodeTreeLeaf)this.Branch.Nodes[accessorNodes.Length + 1]).Text;
        }

        private void CreateGenericsVisitor()
        {
            this.genericsVisitor = (IListVisitor)this.CreateVisitors(new KindFilter(SyntaxKind.TypeArgumentList)).SingleOrDefault();
        }
    }
}