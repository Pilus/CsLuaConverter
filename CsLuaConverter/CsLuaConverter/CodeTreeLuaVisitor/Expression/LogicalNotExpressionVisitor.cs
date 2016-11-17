namespace CsLuaConverter.CodeTreeLuaVisitor.Expression
{
    using CodeTree;
    using Microsoft.CodeAnalysis.CSharp;
    using Providers;

    public class LogicalNotExpressionVisitor : BaseVisitor
    {
        private readonly IVisitor innnerVisitor;

        public LogicalNotExpressionVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.ExpectKind(0, SyntaxKind.ExclamationToken);
            this.innnerVisitor = this.CreateVisitor(1);
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IContext context)
        {
            textWriter.Write("not(");
            this.innnerVisitor.Visit(textWriter, context);
            textWriter.Write(")");
        }
    }
}