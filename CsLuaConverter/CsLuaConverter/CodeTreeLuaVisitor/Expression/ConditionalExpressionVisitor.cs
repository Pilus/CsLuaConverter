namespace CsLuaConverter.CodeTreeLuaVisitor.Expression
{
    using CodeTree;
    using CsLuaConverter.Context;
    using CsLuaConverter.SyntaxExtensions;

    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    public class ConditionalExpressionVisitor : BaseVisitor
    {

        private readonly IVisitor conditionalStatement;

        public ConditionalExpressionVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.ExpectKind(1, SyntaxKind.QuestionToken);

            if (this.Branch.Nodes.Length <= 3)
            {
                this.conditionalStatement = this.CreateVisitor(2);
                return;
            }

            this.ExpectKind(3, SyntaxKind.ColonToken);
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IContext context)
        {
            var syntax = (ConditionalExpressionSyntax)this.Branch.SyntaxNode;

            if (this.conditionalStatement != null)
            {
                textWriter.Write("_M.CA(");
                syntax.Condition.Write(textWriter, context);
                textWriter.Write(",function(obj) return (obj % _M.DOT).");
                this.conditionalStatement.Visit(textWriter, context);
                textWriter.Write("; end)");
                return;
            }

            syntax.Condition.Write(textWriter, context);

            textWriter.Write(" and ");
            syntax.WhenTrue.Write(textWriter, context);

            textWriter.Write(" or ");
            syntax.WhenFalse.Write(textWriter, context);
        }
    }
}