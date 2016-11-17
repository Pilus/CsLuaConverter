namespace CsLuaConverter.CodeTreeLuaVisitor.Statement
{
    using System.Linq;
    using CodeTree;
    using Filters;
    using Microsoft.CodeAnalysis.CSharp;
    using Providers;

    public class TryStatementVisitor : BaseVisitor
    {
        private readonly BlockVisitor block;
        private readonly CatchClauseVisitor[] catches;
        private readonly FinallyClauseVisitor finaly;

        public TryStatementVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.ExpectKind(0, SyntaxKind.TryKeyword);
            this.ExpectKind(1, SyntaxKind.Block);
            this.block = (BlockVisitor) this.CreateVisitor(1);
            this.catches =
                this.CreateVisitors(new KindFilter(SyntaxKind.CatchClause))
                    .Select(v => (CatchClauseVisitor) v)
                    .ToArray();
            this.finaly = (FinallyClauseVisitor)this.CreateVisitors(new KindFilter(SyntaxKind.FinallyClause)).SingleOrDefault();
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IContext context)
        {
            textWriter.WriteLine("_M.Try(");
            textWriter.Indent++;

            textWriter.WriteLine("function()");
            this.block.Visit(textWriter, context);
            textWriter.WriteLine("end,");
            textWriter.WriteLine("{");
            textWriter.Indent++;

            this.catches.VisitAll(textWriter, context);

            textWriter.Indent--;
            textWriter.WriteLine("},");

            if (this.finaly != null)
            {
                textWriter.WriteLine("function()");
                this.finaly.Visit(textWriter, context);
                textWriter.WriteLine("end");
            }
            else
            {
                textWriter.WriteLine("nil");
            }

            textWriter.Indent--;
            textWriter.WriteLine(");");
        }
    }
}