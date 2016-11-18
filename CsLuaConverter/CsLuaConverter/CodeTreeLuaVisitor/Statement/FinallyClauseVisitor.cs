namespace CsLuaConverter.CodeTreeLuaVisitor.Statement
{
    using CodeTree;
    using CsLuaConverter.Context;
    using Microsoft.CodeAnalysis.CSharp;

    public class FinallyClauseVisitor : BaseVisitor
    {
        private readonly BlockVisitor block;
        public FinallyClauseVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.ExpectKind(0, SyntaxKind.FinallyKeyword);
            this.ExpectKind(1, SyntaxKind.Block);
            this.block = (BlockVisitor) this.CreateVisitor(1);
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IContext context)
        {
            this.block.Visit(textWriter, context);
        }
    }
}