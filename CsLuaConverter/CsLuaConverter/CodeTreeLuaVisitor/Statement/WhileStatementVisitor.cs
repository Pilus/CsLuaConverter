namespace CsLuaConverter.CodeTreeLuaVisitor.Statement
{
    using CodeTree;
    using CsLuaConverter.Context;
    using Microsoft.CodeAnalysis.CSharp;

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

        public override void Visit(IIndentedTextWriterWrapper textWriter, IContext context)
        {
            textWriter.Write("while (");
            this.statementVisitor.Visit(textWriter, context);
            textWriter.WriteLine(") do");
            this.bodyVisitor.Visit(textWriter, context);
            textWriter.WriteLine("end");
        }
    }
}