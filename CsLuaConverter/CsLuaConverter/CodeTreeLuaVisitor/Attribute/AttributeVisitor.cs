namespace CsLuaConverter.CodeTreeLuaVisitor.Attribute
{
    using System.CodeDom.Compiler;
    using System.Linq;
    using CodeTree;
    using CsLuaFramework.Attributes;
    using Microsoft.CodeAnalysis.CSharp;
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

        public override void Visit(IndentedTextWriter textWriter, IProviders providers)
        {
            throw new System.NotImplementedException();
        }

        public bool IsCsLuaAddOnAttribute()
        {
            var name = this.name.GetName().Single();
            return name.Equals(nameof(CsLuaAddOnAttribute)) || name.Equals(nameof(CsLuaAddOnAttribute).Replace("Attribute", ""));
        }
    }
}