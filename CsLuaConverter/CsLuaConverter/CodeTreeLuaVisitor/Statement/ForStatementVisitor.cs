namespace CsLuaConverter.CodeTreeLuaVisitor.Statement
{
    using System.Linq;

    using CodeTree;

    using CsLuaConverter.Context;
    using CsLuaConverter.SyntaxExtensions;

    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    public class ForStatementVisitor : BaseVisitor
    {
        private readonly BaseVisitor initialVisitor;
        //private readonly BaseVisitor conditionVisitor;
        //private readonly BaseVisitor increamentVisitor;
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
            //this.conditionVisitor = this.CreateVisitor(4);
            //this.increamentVisitor = this.CreateVisitor(6);
            this.bodyVisitor = this.CreateVisitor(8);
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IContext context)
        {
            var syntax = (ForStatementSyntax)this.Branch.SyntaxNode;

            this.initialVisitor.Visit(textWriter, context);
            textWriter.WriteLine(";");
            textWriter.Write("while (");
            syntax.Condition.Write(textWriter, context);
            textWriter.WriteLine(") do");

            this.bodyVisitor.Visit(textWriter, context);
            syntax.Incrementors.Single().Write(textWriter, context);

            textWriter.WriteLine(";");
            textWriter.WriteLine("end");
        }
    }
}