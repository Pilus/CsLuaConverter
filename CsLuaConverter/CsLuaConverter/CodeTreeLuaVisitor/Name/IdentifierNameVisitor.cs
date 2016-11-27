namespace CsLuaConverter.CodeTreeLuaVisitor.Name
{
    using System.Linq;
    using CodeTree;
    using CsLuaConverter.Context;

    using Microsoft.CodeAnalysis.CSharp.Syntax;

    using NameExtensions = CsLuaConverter.SyntaxExtensions.NameExtensions;

    public class IdentifierNameVisitor : BaseVisitor, INameVisitor
    {
        private readonly string text;

        public IdentifierNameVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.text = ((CodeTreeLeaf)this.Branch.Nodes.Single()).Text;
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IContext context)
        {
            NameExtensions.Write((IdentifierNameSyntax)this.Branch.SyntaxNode, textWriter, context);
        }

        public string[] GetName()
        {
            return new[] { this.text};
        }
    }
}