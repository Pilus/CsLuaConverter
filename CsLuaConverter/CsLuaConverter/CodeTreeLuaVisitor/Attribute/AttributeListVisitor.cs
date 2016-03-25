namespace CsLuaConverter.CodeTreeLuaVisitor.Attribute
{
    using System.CodeDom.Compiler;
    using System.Linq;
    using CodeTree;
    using Filters;
    using Microsoft.CodeAnalysis.CSharp;
    using Providers;

    public class AttributeListVisitor : BaseVisitor
    {
        private readonly AttributeVisitor[] attributes;
        public AttributeListVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.attributes =
                this.CreateVisitors(new KindFilter(SyntaxKind.Attribute))
                    .Select(v => (AttributeVisitor) v)
                    .ToArray();
        }

        public override void Visit(IndentedTextWriter textWriter, IProviders providers)
        {
            throw new System.NotImplementedException();
        }

        public bool HasCsLuaAddOnAttribute()
        {
            return this.attributes.Any(a => a.IsCsLuaAddOnAttribute());
        }
    }
}