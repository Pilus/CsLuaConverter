namespace CsLuaConverter.CodeTreeLuaVisitor.Accessor
{
    using System.Linq;
    using CodeTree;
    using Filters;
    using Microsoft.CodeAnalysis.CSharp;
    using Providers;

    public class GetAccessorDeclarationVisitor : BaseVisitor, IAccessor
    {
        private readonly BlockVisitor block;

        public string AdditionalParameters = string.Empty;

        public GetAccessorDeclarationVisitor(CodeTreeBranch branch) : base(branch)
        {
            var accessor = new KindRangeFilter(null, SyntaxKind.GetKeyword).Filter(this.Branch.Nodes).ToArray();
            this.ExpectKind(accessor.Length, SyntaxKind.GetKeyword);
            if (this.IsKind(accessor.Length + 1, SyntaxKind.Block))
            {
                this.block = (BlockVisitor)this.CreateVisitor(1);
            }
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IContext context)
        {
            if (this.block == null)
            {
                return;
            }

            textWriter.WriteLine($"get = function(element{this.AdditionalParameters})");
            this.block.Visit(textWriter, context);
            textWriter.WriteLine("end,");
        }

        public bool IsAutoProperty()
        {
            return this.block == null;
        }
    }
}