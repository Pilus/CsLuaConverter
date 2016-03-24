namespace CsLuaConverter.CodeTreeLuaVisitor.Accessor
{
    using System.CodeDom.Compiler;
    using System.Linq;
    using CodeTree;
    using Filters;
    using Microsoft.CodeAnalysis.CSharp;
    using Providers;

    public class GetAccessorDeclarationVisitor : BaseVisitor, IAccessor
    {
        private readonly BlockVisitor block;
        public GetAccessorDeclarationVisitor(CodeTreeBranch branch) : base(branch)
        {
            var accessor = new KindRangeFilter(null, SyntaxKind.GetKeyword).Filter(this.Branch.Nodes).ToArray();
            this.ExpectKind(accessor.Length, SyntaxKind.GetKeyword);
            if (this.IsKind(accessor.Length + 1, SyntaxKind.Block))
            {
                this.block = (BlockVisitor)this.CreateVisitor(1);
            }
        }

        public override void Visit(IndentedTextWriter textWriter, IProviders providers)
        {
            if (this.block == null)
            {
                return;
            }

            textWriter.WriteLine("get = function(element)");
            this.block.Visit(textWriter, providers);
            textWriter.WriteLine("end,");
        }

        public bool IsAutoProperty()
        {
            return this.block == null;
        }
    }
}