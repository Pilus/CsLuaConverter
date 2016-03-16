namespace CsLuaConverter.CodeTreeLuaVisitor.Elements
{
    using System.CodeDom.Compiler;
    using System.Linq;
    using CodeTree;
    using Filters;
    using Lists;
    using Microsoft.CodeAnalysis.CSharp;
    using Name;
    using Providers;

    public class ClassDeclarationVisitor : BaseVisitor, IElementVisitor
    {
        private string name;
        private IListVisitor genericsVisitor;

        public ClassDeclarationVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.CreateNameVisitor();
            this.CreateGenericsVisitor();
        }

        public override void Visit(IndentedTextWriter textWriter, IProviders providers)
        {
            var originalScope = providers.NameProvider.CloneScope();
            providers.NameProvider.AddAllInheritedMembersToScope(this.name);

            //textWriter.WriteLine("[{0}] = function(interactionElement, generics, staticValues)", GetNumOfGenerics(element));
            //textWriter.Indent++;

            //throw new System.NotImplementedException();

            providers.NameProvider.SetScope(originalScope);
        }

        public string GetName()
        {
            return this.name;
        }

        private void CreateNameVisitor()
        {
            var accessorNodes = this.GetFilteredNodes(new KindRangeFilter(null, SyntaxKind.ClassKeyword));
            this.ExpectKind(accessorNodes.Length, SyntaxKind.ClassKeyword);
            this.ExpectKind(accessorNodes.Length + 1, SyntaxKind.IdentifierToken);
            this.name = ((CodeTreeLeaf) this.Branch.Nodes[accessorNodes.Length + 1]).Text;
        }

        private void CreateGenericsVisitor()
        {
            this.genericsVisitor = (IListVisitor)this.CreateVisitors(new KindFilter(SyntaxKind.TypeArgumentList)).SingleOrDefault();
        }

        public int GetNumOfGenerics()
        {
            return this.genericsVisitor?.GetNumElements() ?? 0;
        }
    }
}