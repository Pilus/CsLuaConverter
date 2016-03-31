namespace CsLuaConverter.CodeTreeLuaVisitor
{
    using System.CodeDom.Compiler;
    using CodeTree;
    using Filters;
    using Microsoft.CodeAnalysis.CSharp;
    using Providers;

    public class BlockVisitor : BaseVisitor
    {
        private readonly BaseVisitor[] visitors;

        public BlockVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.visitors = this.CreateVisitors(new KindRangeFilter(SyntaxKind.OpenBraceToken, SyntaxKind.CloseBraceToken));
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            textWriter.Indent++;
            this.visitors.VisitAll(textWriter, providers);
            textWriter.Indent--;
        }
    }
}