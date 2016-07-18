namespace CsLuaConverter.CodeTreeLuaVisitor.Statement
{
    using CodeTree;
    using Microsoft.CodeAnalysis.CSharp;
    using Providers;

    public class ReturnStatementVisitor : BaseVisitor
    {
        private readonly BaseVisitor innerVisitor;
        public ReturnStatementVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.ExpectKind(0, SyntaxKind.ReturnKeyword);

            if (this.Branch.Nodes.Length == 2)
            {
                this.ExpectKind(1, SyntaxKind.SemicolonToken);
                return;
            }

            this.ExpectKind(2, SyntaxKind.SemicolonToken);
            this.innerVisitor = this.CreateVisitor(1);
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            textWriter.Write("return ");
            providers.TypeKnowledgeRegistry.CurrentType = null;
            this.innerVisitor?.Visit(textWriter, providers);
            textWriter.WriteLine(";");
            providers.TypeKnowledgeRegistry.CurrentType = null;
        }
    }
}