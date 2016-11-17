namespace CsLuaConverter.CodeTreeLuaVisitor
{
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

        public override void Visit(IIndentedTextWriterWrapper textWriter, IContext context)
        {
            textWriter.Write(" = ");
            this.innerVisitor.Visit(textWriter, context);
        }
    }
}