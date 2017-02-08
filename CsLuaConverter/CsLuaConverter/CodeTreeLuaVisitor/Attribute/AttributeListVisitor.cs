namespace CsLuaConverter.CodeTreeLuaVisitor.Attribute
{
    using System.Linq;
    using CodeTree;
    using CsLuaConverter.Context;
    using CsLuaConverter.SyntaxExtensions;
    using Filters;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    public class AttributeListVisitor : SyntaxVisitorBase<AttributeListSyntax>
    {
        private readonly AttributeVisitor[] attributes;
        public AttributeListVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.attributes =
                this.CreateVisitors(new KindFilter(SyntaxKind.Attribute))
                    .Select(v => (AttributeVisitor) v)
                    .ToArray();
        }

        public AttributeListVisitor(AttributeListSyntax syntax) : base(syntax)
        {
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IContext context)
        {
            this.Syntax.Visit(textWriter, context);
        }

        public bool HasCsLuaAddOnAttribute()
        {
            return this.attributes.Any(a => a.IsCsLuaAddOnAttribute());
        }
    }
}