namespace CsLuaConverter.CodeTreeLuaVisitor.Name
{
    using System.Linq;
    using CodeTree;
    using CsLuaSyntaxTranslator;
    using CsLuaSyntaxTranslator.Context;
    using CsLuaSyntaxTranslator.SyntaxExtensions;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    using NameExtensions = CsLuaSyntaxTranslator.SyntaxExtensions.NameExtensions;

    public class IdentifierNameVisitor : BaseVisitor, INameVisitor
    {
        private readonly string text;

        public IdentifierNameVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.text = ((CodeTreeLeaf)this.Branch.Nodes.Single()).Text;
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IContext context)
        {
            var syntax = (IdentifierNameSyntax)this.Branch.SyntaxNode;
            syntax.Write(textWriter, context);
        }

        public string[] GetName()
        {
            return new[] { this.text};
        }
    }
}