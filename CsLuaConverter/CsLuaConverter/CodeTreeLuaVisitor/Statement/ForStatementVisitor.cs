namespace CsLuaConverter.CodeTreeLuaVisitor.Statement
{
    using CodeTree;
    using Providers;
    using System.Linq;
    using Microsoft.CodeAnalysis.CSharp;

    public class ForStatementVisitor : BaseVisitor
    {
        private readonly BaseVisitor initialVisitor;
        private readonly BaseVisitor conditionVisitor;
        private readonly BaseVisitor increamentVisitor;
        private readonly BaseVisitor bodyVisitor;

        public ForStatementVisitor(CodeTreeBranch branch) : base(branch)
        {
            // Example: ForKeyword,OpenParenToken,VariableDeclaration,SemicolonToken,LessThanOrEqualExpression,SemicolonToken,PostIncrementExpression,CloseParenToken,Block
            this.ExpectKind(0, SyntaxKind.ForKeyword);
            this.ExpectKind(1, SyntaxKind.OpenParenToken);
            this.ExpectKind(3, SyntaxKind.SemicolonToken);
            this.ExpectKind(5, SyntaxKind.SemicolonToken);
            this.ExpectKind(7, SyntaxKind.CloseParenToken);
            this.initialVisitor = this.CreateVisitor(2);
            this.conditionVisitor = this.CreateVisitor(4);
            this.increamentVisitor = this.CreateVisitor(6);
            this.bodyVisitor = this.CreateVisitor(8);
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            this.initialVisitor.Visit(textWriter, providers);
            textWriter.WriteLine(";");
            textWriter.Write("while (");
            this.conditionVisitor.Visit(textWriter, providers);
            textWriter.WriteLine(") do");

            this.bodyVisitor.Visit(textWriter, providers);
            this.increamentVisitor.Visit(textWriter, providers);

            textWriter.WriteLine(";");
            textWriter.WriteLine("end");
        }
    }
}