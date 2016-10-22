namespace CsLuaConverter.CodeTreeLuaVisitor.Statement
{
    using CodeTree;
    using Microsoft.CodeAnalysis.CSharp;
    using Providers;

    public class ThrowStatementVisitor : BaseVisitor
    {
        private readonly IVisitor inner;
        public ThrowStatementVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.ExpectKind(0, SyntaxKind.ThrowKeyword);
            this.inner = this.CreateVisitor(1);
            this.ExpectKind(2, SyntaxKind.SemicolonToken);
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            textWriter.Write("_M.Throw(");
            this.inner.Visit(textWriter, providers);
            textWriter.WriteLine(");");
        }
    }
}