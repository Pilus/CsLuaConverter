namespace CsLuaConverter.CodeTreeLuaVisitor.Attribute
{
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

        public override void Visit(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            textWriter.WriteLine("local attributes = {");
            textWriter.Indent++;

            this.attributes.VisitAll(textWriter, providers);

            textWriter.Indent--;
            textWriter.WriteLine("};");
        }

        public bool HasCsLuaAddOnAttribute()
        {
            return this.attributes.Any(a => a.IsCsLuaAddOnAttribute());
        }
    }
}