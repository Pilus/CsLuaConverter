namespace CsLuaConverter.CodeTreeLuaVisitor.Filters
{
    using System.Collections.Generic;
    using System.Linq;
    using CodeTree;
    using Microsoft.CodeAnalysis.CSharp;

    public class KindRangeFilter : INodeFilter
    {
        private readonly SyntaxKind? startKind;
        private readonly SyntaxKind? endKind;
        private readonly SyntaxKind[] excludedKinds;

        public KindRangeFilter(SyntaxKind? startKind, SyntaxKind? endKind)
        {
            this.startKind = startKind;
            this.endKind = endKind;
        }

        public KindRangeFilter(SyntaxKind? startKind, SyntaxKind? endKind, params SyntaxKind[] excludedKinds)
        {
            this.startKind = startKind;
            this.endKind = endKind;
            this.excludedKinds = excludedKinds;
        }


        public IEnumerable<CodeTreeNode> Filter(IEnumerable<CodeTreeNode> nodes)
        {
            var withInLimits = this.startKind == null;
            return nodes.Where(node =>
            {
                if (node.Kind == this.startKind && node.Kind != this.endKind)
                {
                    withInLimits = true;
                }
                else if (node.Kind == this.endKind)
                {
                    withInLimits = false;
                }

                return withInLimits && (this.excludedKinds == null || !this.excludedKinds.Contains(node.Kind));
            });
        }
    }
}