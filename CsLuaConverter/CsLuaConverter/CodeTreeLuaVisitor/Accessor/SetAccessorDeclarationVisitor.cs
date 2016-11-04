namespace CsLuaConverter.CodeTreeLuaVisitor.Accessor
{
    using System.Linq;
    using CodeTree;
    using Filters;
    using Microsoft.CodeAnalysis.CSharp;
    using Providers;
    using Providers.TypeProvider;

    public class SetAccessorDeclarationVisitor : BaseVisitor, IAccessor
    {
        private readonly BlockVisitor block;

        public string AdditionalParameters = string.Empty;

        public SetAccessorDeclarationVisitor(CodeTreeBranch branch) : base(branch)
        {
            var accessor = new KindRangeFilter(null, SyntaxKind.SetKeyword).Filter(this.Branch.Nodes).ToArray();
            this.ExpectKind(accessor.Length, SyntaxKind.SetKeyword);
            if (this.IsKind(accessor.Length + 1, SyntaxKind.Block))
            {
                this.block = (BlockVisitor) this.CreateVisitor(1);
            }
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            if (this.block == null)
            {
                return;
            }

            textWriter.WriteLine($"set = function(element{this.AdditionalParameters} , value)");
            this.block.Visit(textWriter, providers);
            textWriter.WriteLine("end,");
        }
    
        public bool IsAutoProperty()
        {
            return this.block == null;
        }
    }
}