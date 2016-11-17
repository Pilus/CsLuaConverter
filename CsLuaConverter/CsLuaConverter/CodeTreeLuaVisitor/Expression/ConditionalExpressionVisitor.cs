namespace CsLuaConverter.CodeTreeLuaVisitor.Expression
{
    using CodeTree;
    using Microsoft.CodeAnalysis.CSharp;
    using Providers;

    public class ConditionalExpressionVisitor : BaseVisitor
    {
        private readonly IVisitor condition;
        private readonly IVisitor trueStatement;
        private readonly IVisitor falseStatement;

        private readonly IVisitor conditionalStatement;

        public ConditionalExpressionVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.ExpectKind(1, SyntaxKind.QuestionToken);
            this.condition = this.CreateVisitor(0);

            if (this.Branch.Nodes.Length <= 3)
            {
                this.conditionalStatement = this.CreateVisitor(2);
                return;
            }

            this.ExpectKind(3, SyntaxKind.ColonToken);
            this.trueStatement = this.CreateVisitor(2);
            this.falseStatement = this.CreateVisitor(4);
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IContext context)
        {
            if (this.conditionalStatement != null)
            {
                textWriter.Write("_M.CA(");
                this.condition.Visit(textWriter, context);
                textWriter.Write(",function(obj) return (obj % _M.DOT).");
                this.conditionalStatement.Visit(textWriter, context);
                textWriter.Write("; end)");
                return;
            }

            this.condition.Visit(textWriter, context);

            textWriter.Write(" and ");
            this.trueStatement.Visit(textWriter, context);

            textWriter.Write(" or ");
            this.falseStatement.Visit(textWriter, context);
        }
    }
}