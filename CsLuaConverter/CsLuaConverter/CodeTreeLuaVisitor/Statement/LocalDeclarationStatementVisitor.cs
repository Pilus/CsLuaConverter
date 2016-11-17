namespace CsLuaConverter.CodeTreeLuaVisitor.Statement
{
    using CodeTree;
    using Member;
    using Microsoft.CodeAnalysis.CSharp;
    using Providers;

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

        public override void Visit(IIndentedTextWriterWrapper textWriter, IContext context)
        {
            this.variableDeclarationVisitor.Visit(textWriter, context);
            textWriter.WriteLine(";");
        }
    }
}