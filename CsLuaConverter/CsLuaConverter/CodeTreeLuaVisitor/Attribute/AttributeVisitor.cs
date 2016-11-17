namespace CsLuaConverter.CodeTreeLuaVisitor.Attribute
{
    using System.Linq;
    using CodeTree;
    using CsLuaFramework.Attributes;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    using Name;
    using Providers;

    public class AttributeVisitor : BaseVisitor
    {
        private readonly IdentifierNameVisitor name;
        public AttributeVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.ExpectKind(0, SyntaxKind.IdentifierName);
            this.name = (IdentifierNameVisitor) this.CreateVisitor(0);
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IContext context)
        {
            var symbol = context.SemanticModel.GetSymbolInfo(this.Branch.SyntaxNode as AttributeSyntax).Symbol;
            context.TypeReferenceWriter.WriteTypeReference(symbol.ContainingType, textWriter);
        }

        public bool IsCsLuaAddOnAttribute()
        {
            var name = this.name.GetName().Single();
            return name.Equals(nameof(CsLuaAddOnAttribute)) || name.Equals(nameof(CsLuaAddOnAttribute).Replace("Attribute", ""));
        }
    }
}