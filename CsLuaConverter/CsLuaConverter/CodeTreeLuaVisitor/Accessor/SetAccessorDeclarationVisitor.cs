namespace CsLuaConverter.CodeTreeLuaVisitor.Accessor
{
    using System.CodeDom.Compiler;
    using System.Linq;
    using CodeTree;
    using Filters;
    using Microsoft.CodeAnalysis.CSharp;
    using Providers;
    using Providers.TypeProvider;

    public class SetAccessorDeclarationVisitor : BaseVisitor, IAccessor
    {
        private readonly BlockVisitor block;
        public SetAccessorDeclarationVisitor(CodeTreeBranch branch) : base(branch)
        {
            var accessor = new KindRangeFilter(null, SyntaxKind.SetKeyword).Filter(this.Branch.Nodes).ToArray();
            this.ExpectKind(accessor.Length, SyntaxKind.SetKeyword);
            if (this.IsKind(accessor.Length + 1, SyntaxKind.Block))
            {
                this.block = (BlockVisitor) this.CreateVisitor(1);
            }
        }

        public override void Visit(IndentedTextWriter textWriter, IProviders providers)
        {
            if (this.block == null)
            {
                return;
            }

            var scope = providers.NameProvider.CloneScope();
            providers.NameProvider.AddToScope(new ScopeElement("value", providers.TypeKnowledgeRegistry.CurrentType));
            textWriter.WriteLine("set = function(element, value)");
            this.block.Visit(textWriter, providers);
            textWriter.WriteLine("end,");
            providers.NameProvider.SetScope(scope);
        }
    
        public bool IsAutoProperty()
        {
            return this.block == null;
        }
    }
}