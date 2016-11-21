namespace CsLuaConverter.CodeTreeLuaVisitor.Attribute
{
    using System.Linq;
    using CodeTree;
    using CsLuaConverter.Context;
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
            textWriter.WriteLine("local attributes = {");
            textWriter.Indent++;

            VisitAllNodes(this.Syntax.Attributes, textWriter, context);

            textWriter.Indent--;
            textWriter.WriteLine("};");
        }

        public bool HasCsLuaAddOnAttribute()
        {
            return this.attributes.Any(a => a.IsCsLuaAddOnAttribute());
        }
    }
}