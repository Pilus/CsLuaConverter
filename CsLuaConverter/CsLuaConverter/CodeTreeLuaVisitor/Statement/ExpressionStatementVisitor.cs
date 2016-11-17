namespace CsLuaConverter.CodeTreeLuaVisitor.Statement
{
    using CodeTree;
    using CsLuaConverter.Context;
    using Microsoft.CodeAnalysis.CSharp;

    public class ExpressionStatementVisitor : BaseVisitor
    {
        private readonly BaseVisitor innerVisitor;
        public ExpressionStatementVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.ExpectKind(1, SyntaxKind.SemicolonToken);
            this.innerVisitor = this.CreateVisitor(0);
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IContext context)
        {
            this.innerVisitor.Visit(textWriter, context);
            textWriter.WriteLine(";");
        }
    }
}