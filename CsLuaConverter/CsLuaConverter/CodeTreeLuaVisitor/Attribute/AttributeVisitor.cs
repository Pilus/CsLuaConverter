namespace CsLuaConverter.CodeTreeLuaVisitor.Attribute
{
    using System.Linq;
    using CodeTree;
    using CsLuaConverter.Context;
    using CsLuaFramework.Attributes;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    using Name;

    public class AttributeVisitor : SyntaxVisitorBase<AttributeSyntax>
    {
        private readonly IdentifierNameVisitor name;
        public AttributeVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.ExpectKind(0, SyntaxKind.IdentifierName);
            this.name = (IdentifierNameVisitor) this.CreateVisitor(0);
        }

        public AttributeVisitor(AttributeSyntax syntax) : base(syntax)
        {
            
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IContext context)
        {
            Visit(this.Syntax, textWriter, context);
        }

        public static void Visit(AttributeSyntax syntax, IIndentedTextWriterWrapper textWriter, IContext context)
        {
            var symbol = context.SemanticModel.GetSymbolInfo(syntax).Symbol;
            context.TypeReferenceWriter.WriteTypeReference(symbol.ContainingType, textWriter);
        }

        public bool IsCsLuaAddOnAttribute()
        {
            var name = this.name.GetName().Single();
            return name.Equals(nameof(CsLuaAddOnAttribute)) || name.Equals(nameof(CsLuaAddOnAttribute).Replace("Attribute", ""));
        }
    }
}