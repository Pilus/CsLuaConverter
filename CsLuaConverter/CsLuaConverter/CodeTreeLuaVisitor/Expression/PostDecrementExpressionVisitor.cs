namespace CsLuaConverter.CodeTreeLuaVisitor.Expression
{
    using CodeTree;
    using Microsoft.CodeAnalysis.CSharp;
    using Providers;

    public class PostDecrementExpressionVisitor : BaseVisitor
    {
        private readonly IVisitor target;

        public PostDecrementExpressionVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.ExpectKind(1, SyntaxKind.MinusMinusToken);
            this.target = this.CreateVisitor(0);
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            this.target.Visit(textWriter, providers);
            textWriter.Write(" = ");

            this.target.Visit(textWriter, providers);
            textWriter.Write(" - 1");
        }
    }
}