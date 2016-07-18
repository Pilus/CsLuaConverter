namespace CsLuaConverter.CodeTreeLuaVisitor.Statement
{
    using CodeTree;
    using Member;
    using Microsoft.CodeAnalysis.CSharp;
    using Providers;
    using Providers.GenericsRegistry;

    public class LocalDeclarationStatementVisitor : BaseVisitor
    {
        private readonly VariableDeclarationVisitor variableDeclarationVisitor;
        public LocalDeclarationStatementVisitor(CodeTreeBranch branch) : base(branch)
        {
            var i = 0;

            if (this.IsKind(i, SyntaxKind.ConstKeyword))
            {
                i++;
            }

            this.ExpectKind(i, SyntaxKind.VariableDeclaration);
            this.ExpectKind(i+1, SyntaxKind.SemicolonToken);
            this.variableDeclarationVisitor = (VariableDeclarationVisitor)this.CreateVisitor(i);
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            this.variableDeclarationVisitor.Visit(textWriter, providers);
            providers.TypeKnowledgeRegistry.CurrentType = null;
            providers.GenericsRegistry.ClearScope(GenericScope.Invocation);
            textWriter.WriteLine(";");
        }
    }
}