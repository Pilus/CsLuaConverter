namespace CsLuaConverter.CodeTreeLuaVisitor
{
    using CodeTree;
    using CsLuaConverter.Context;
    using Filters;
    using Microsoft.CodeAnalysis.CSharp;

    public class BlockVisitor : BaseVisitor
    {
        private readonly BaseVisitor[] visitors;

        public BlockVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.visitors = this.CreateVisitors(new KindRangeFilter(SyntaxKind.OpenBraceToken, SyntaxKind.CloseBraceToken));
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IContext context)
        {
            textWriter.Indent++;
            this.visitors.VisitAll(textWriter, context);
            textWriter.Indent--;
        }
    }
}