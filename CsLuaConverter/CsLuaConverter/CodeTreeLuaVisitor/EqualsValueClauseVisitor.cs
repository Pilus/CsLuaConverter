namespace CsLuaConverter.CodeTreeLuaVisitor
{
    using System.CodeDom.Compiler;
    using CodeTree;
    using Microsoft.CodeAnalysis.CSharp;
    using Providers;

    public class EqualsValueClauseVisitor : BaseVisitor
    {
        private readonly BaseVisitor innerVisitor;

        public EqualsValueClauseVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.ExpectKind(0, SyntaxKind.EqualsToken);
            this.innerVisitor = this.CreateVisitor(1);
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            textWriter.Write(" = ");
            this.innerVisitor.Visit(textWriter, providers);
        }
    }
}