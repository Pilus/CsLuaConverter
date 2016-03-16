namespace CsLuaConverter.CodeTreeLuaVisitor.Elements
{
    using System.CodeDom.Compiler;
    using CodeTree;
    using Filters;
    using Microsoft.CodeAnalysis.CSharp;
    using Providers;

    public class EnumDeclarationVisitor : BaseVisitor, IElementVisitor
    {
        private string name;

        public EnumDeclarationVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.CreateNameVisitor();
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
            return 0;
        }

        private void CreateNameVisitor()
        {
            var accessorNodes = this.GetFilteredNodes(new KindRangeFilter(null, SyntaxKind.EnumKeyword));
            this.ExpectKind(accessorNodes.Length, SyntaxKind.EnumKeyword);
            this.ExpectKind(accessorNodes.Length + 1, SyntaxKind.IdentifierToken);
            this.name = ((CodeTreeLeaf)this.Branch.Nodes[accessorNodes.Length + 1]).Text;
        }
    }
}