namespace CsLuaConverter.CodeTreeLuaVisitor.Statement
{
    using CodeTree;
    using Microsoft.CodeAnalysis.CSharp;
    using Providers;

    public class CatchClauseVisitor : BaseVisitor
    {
        private readonly CatchDeclarationVisitor declaration;
        private readonly BlockVisitor block;
        public CatchClauseVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.ExpectKind(0, SyntaxKind.CatchKeyword);
            var i = 1;
            if (this.IsKind(i, SyntaxKind.CatchDeclaration))
            {
                this.declaration = (CatchDeclarationVisitor)this.CreateVisitor(i);
                i++;
            }
            
            this.ExpectKind(i, SyntaxKind.Block);
            this.block = (BlockVisitor)this.CreateVisitor(i);
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            var scope = providers.NameProvider.CloneScope();

            textWriter.WriteLine("{");
            textWriter.Indent++;

            if (this.declaration != null)
            {
                this.declaration.Visit(textWriter, providers);
            }
            else
            {
                textWriter.WriteLine("func = function()");
            }

            providers.TypeKnowledgeRegistry.CurrentType = null;
            this.block.Visit(textWriter, providers);
            
            textWriter.WriteLine("end,");
            
            textWriter.Indent--;
            textWriter.WriteLine("},");

            providers.NameProvider.SetScope(scope);
        }
    }
}