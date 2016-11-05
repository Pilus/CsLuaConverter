namespace CsLuaConverter.CodeTreeLuaVisitor.Expression
{
    using CsLuaConverter.CodeTree;
    using CsLuaConverter.Providers;

    using Microsoft.CodeAnalysis.CSharp;

    public class ConditionalAccessExpressionVisitor : BaseVisitor
    {
        private BaseVisitor valueVisitor;
        private BaseVisitor innerVisitor;

        public ConditionalAccessExpressionVisitor(CodeTreeBranch branch)
            : base(branch)
        {
            this.ExpectKind(1, SyntaxKind.QuestionToken);
            this.valueVisitor = this.CreateVisitor(0);
            this.innerVisitor = this.CreateVisitor(2);
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            textWriter.Write("_M.CA(");
            this.valueVisitor.Visit(textWriter, providers);
            textWriter.Write(",function(obj) return (obj % _M.DOT)");
            this.innerVisitor.Visit(textWriter, providers);
            textWriter.Write("; end)");
        }
    }
}