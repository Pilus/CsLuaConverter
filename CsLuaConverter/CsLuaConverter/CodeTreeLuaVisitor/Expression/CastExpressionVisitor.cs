namespace CsLuaConverter.CodeTreeLuaVisitor.Expression
{
    using CodeTree;
    using CsLuaConverter.Context;
    using Microsoft.CodeAnalysis.CSharp;

    public class CastExpressionVisitor : BaseVisitor
    {
        private readonly IVisitor innerVisitor;

        public CastExpressionVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.ExpectKind(0, SyntaxKind.OpenParenToken);
            this.ExpectKind(2, SyntaxKind.CloseParenToken);
            this.innerVisitor = this.CreateVisitor(3);
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IContext context)
        {
            this.innerVisitor.Visit(textWriter, context);
        }
    }
}