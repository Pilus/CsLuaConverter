namespace CsLuaConverter.CodeTreeLuaVisitor.Filters
{
    using System.Collections.Generic;
    using System.Linq;
    using CodeTree;
    using Microsoft.CodeAnalysis.CSharp;

    public class KindFilter : INodeFilter
    {
        private readonly SyntaxKind[] kinds;

        public KindFilter(params SyntaxKind[] kinds)
        {
            this.kinds = kinds;
        }

        public IEnumerable<CodeTreeNode> Filter(IEnumerable<CodeTreeNode> nodes)
        {
            return nodes.Where(n =>  this.kinds.Contains(n.Kind));
        }
    }
}