namespace CsLuaConverter.CodeTreeLuaVisitor.Statement
{
    using CodeTree;
    using Microsoft.CodeAnalysis.CSharp;
    using Providers;

    public class WhileStatementVisitor : BaseVisitor
    {
        private readonly BaseVisitor statementVisitor;
        private readonly BaseVisitor bodyVisitor;

        public WhileStatementVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.ExpectKind(0, SyntaxKind.WhileKeyword);
            this.ExpectKind(1, SyntaxKind.OpenParenToken);
            this.ExpectKind(3, SyntaxKind.CloseParenToken);
            this.statementVisitor = this.CreateVisitor(2);
            this.bodyVisitor = this.CreateVisitor(4);
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            textWriter.Write("while (");
            this.statementVisitor.Visit(textWriter, providers);
            textWriter.WriteLine(") do");
            this.bodyVisitor.Visit(textWriter, providers);
            textWriter.WriteLine("end");
        }
    }
}