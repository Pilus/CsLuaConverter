namespace CsLuaConverter.CodeTreeLuaVisitor.Statement
{
    using CodeTree;
    using CsLuaConverter.Context;
    using Microsoft.CodeAnalysis.CSharp;

    public class IfStatementVisitor : BaseVisitor
    {
        private readonly IVisitor expression;
        private readonly BlockVisitor block;
        private readonly ElseClauseVisitor elseCause;
        private readonly BaseVisitor singleLineVisitor;

        public IfStatementVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.ExpectKind(0, SyntaxKind.IfKeyword);
            this.ExpectKind(1, SyntaxKind.OpenParenToken);
            this.expression = this.CreateVisitor(2);
            this.ExpectKind(3, SyntaxKind.CloseParenToken);

            if (this.IsKind(4, SyntaxKind.Block))
            {
                this.ExpectKind(4, SyntaxKind.Block);
                this.block = (BlockVisitor)this.CreateVisitor(4);
            }
            else
            {
                this.singleLineVisitor = this.CreateVisitor(4);
            }

            if (this.IsKind(5, SyntaxKind.ElseClause))
            {
                this.elseCause = (ElseClauseVisitor)this.CreateVisitor(5);
            }
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IContext context)
        {
            textWriter.Write("if (");
            this.expression.Visit(textWriter, context);
            textWriter.WriteLine(") then");

            this.block?.Visit(textWriter, context);
            this.singleLineVisitor?.Visit(textWriter, context);

            if (this.elseCause != null)
            {
                this.elseCause.Visit(textWriter, context);
            }
            else
            {
                textWriter.WriteLine("end");
            }
        }
    }
}