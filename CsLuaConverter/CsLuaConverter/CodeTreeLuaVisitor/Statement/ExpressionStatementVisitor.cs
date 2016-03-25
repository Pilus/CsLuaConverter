namespace CsLuaConverter.CodeTreeLuaVisitor.Statement
{
    using System.CodeDom.Compiler;
    using CodeTree;
    using Microsoft.CodeAnalysis.CSharp;
    using Providers;

    public class ExpressionStatementVisitor : BaseVisitor
    {
        private readonly BaseVisitor innerVisitor;
        public ExpressionStatementVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.ExpectKind(1, SyntaxKind.SemicolonToken);
            this.innerVisitor = this.CreateVisitor(0);
        }

        public override void Visit(IndentedTextWriter textWriter, IProviders providers)
        {
            this.innerVisitor.Visit(textWriter, providers);
            textWriter.WriteLine(";");
        }
    }
}