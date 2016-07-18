namespace CsLuaConverter.CodeTreeLuaVisitor.Statement
{
    using CodeTree;
    using Microsoft.CodeAnalysis.CSharp;
    using Providers;

    public class ElseClauseVisitor : BaseVisitor
    {
        private readonly BlockVisitor block;
        private readonly IfStatementVisitor elseIfStatement;
        public ElseClauseVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.ExpectKind(0, SyntaxKind.ElseKeyword);
            this.ExpectKind(1, SyntaxKind.Block, SyntaxKind.IfStatement);

            var visitor = this.CreateVisitor(1);
            this.block = visitor as BlockVisitor;
            this.elseIfStatement = visitor as IfStatementVisitor;
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            textWriter.WriteLine("else");
            this.elseIfStatement?.Visit(textWriter, providers);
            this.block?.Visit(textWriter, providers);

            if (this.elseIfStatement != null)
            {
                textWriter.WriteLine("end");
            }
        }
    }
}