namespace CsLuaConverter.CodeTreeLuaVisitor.Statement
{
    using CodeTree;
    using Microsoft.CodeAnalysis.CSharp;
    using Providers;

    public class ElseClauseVisitor : BaseVisitor
    {
        private readonly IVisitor block;
        private readonly IfStatementVisitor elseIfStatement;
        public ElseClauseVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.ExpectKind(0, SyntaxKind.ElseKeyword);

            var visitor = this.CreateVisitor(1);
            this.elseIfStatement = visitor as IfStatementVisitor;

            if (!(visitor is IfStatementVisitor))
            {
                this.block = visitor;
            }
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IContext context)
        {
            textWriter.Write("else");
            if (this.elseIfStatement != null)
            {
                this.elseIfStatement.Visit(textWriter, context);
            }
            else
            {
                textWriter.WriteLine("");
            }
            
            this.block?.Visit(textWriter, context);

            if (this.elseIfStatement == null)
            {
                textWriter.WriteLine("end");
            }
        }
    }
}