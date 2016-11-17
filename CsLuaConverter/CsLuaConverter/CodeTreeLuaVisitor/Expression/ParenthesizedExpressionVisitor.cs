namespace CsLuaConverter.CodeTreeLuaVisitor.Expression
{
    using CodeTree;
    using Microsoft.CodeAnalysis.CSharp;
    using Providers;

    public class ParenthesizedExpressionVisitor : BaseVisitor
    {
        private readonly IVisitor innerVisitor;
        public ParenthesizedExpressionVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.ExpectKind(0, SyntaxKind.OpenParenToken);
            this.ExpectKind(2, SyntaxKind.CloseParenToken);
            this.innerVisitor = this.CreateVisitor(1);
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IContext context)
        {
            textWriter.Write("(");
            this.innerVisitor.Visit(textWriter, context);
            textWriter.Write(")");
        }
    }
}