namespace CsLuaConverter.CodeTreeLuaVisitor.Expression
{
    using CodeTree;
    using Microsoft.CodeAnalysis.CSharp;
    using Providers;

    public class UnaryPlusExpressionVisitor : BaseVisitor
    {
        private readonly IVisitor target;
        public UnaryPlusExpressionVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.ExpectKind(0, SyntaxKind.PlusToken);
            this.target = this.CreateVisitor(1);
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            textWriter.Write("+");
            this.target.Visit(textWriter, providers);
        }
    }
}