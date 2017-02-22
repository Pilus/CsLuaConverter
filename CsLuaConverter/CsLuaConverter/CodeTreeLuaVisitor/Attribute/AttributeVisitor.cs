namespace CsLuaConverter.CodeTreeLuaVisitor.Attribute
{
    using System.Linq;
    using CodeTree;
    using CsLuaFramework.Attributes;
    using CsLuaSyntaxTranslator;
    using CsLuaSyntaxTranslator.Context;
    using CsLuaSyntaxTranslator.SyntaxExtensions;
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
            this.Syntax.Write(textWriter, context);
        }

        public bool IsCsLuaAddOnAttribute()
        {
            var name = this.Syntax.Name.GetText().ToString();
            return name.Equals(nameof(CsLuaAddOnAttribute)) || name.Equals(nameof(CsLuaAddOnAttribute).Replace("Attribute", ""));
        }
    }
}