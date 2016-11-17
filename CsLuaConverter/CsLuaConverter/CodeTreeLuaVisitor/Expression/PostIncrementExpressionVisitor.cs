namespace CsLuaConverter.CodeTreeLuaVisitor.Expression
{
    using CodeTree;
    using CsLuaConverter.Context;
    using Microsoft.CodeAnalysis.CSharp;

    public class PostIncrementExpressionVisitor : BaseVisitor
    {
        private readonly IVisitor target;

        public PostIncrementExpressionVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.ExpectKind(1, SyntaxKind.PlusPlusToken);
            this.target = this.CreateVisitor(0);
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IContext context)
        {
            this.target.Visit(textWriter, context);
            textWriter.Write(" = ");

            this.target.Visit(textWriter, context);
            textWriter.Write(" + 1");
        }
    }
}