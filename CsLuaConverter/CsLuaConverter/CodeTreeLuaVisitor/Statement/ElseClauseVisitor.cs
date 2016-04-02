namespace CsLuaConverter.CodeTreeLuaVisitor.Statement
{
    using CodeTree;
    using Microsoft.CodeAnalysis.CSharp;
    using Providers;

    public class ElseClauseVisitor : BaseVisitor
    {
        private readonly BlockVisitor block;
        public ElseClauseVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.ExpectKind(0, SyntaxKind.ElseKeyword);
            this.ExpectKind(1, SyntaxKind.Block);
            this.block = (BlockVisitor) this.CreateVisitor(1);
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            textWriter.WriteLine("else");
            this.block.Visit(textWriter, providers);
            textWriter.WriteLine("end");
        }
    }
}