namespace CsLuaConverter.CodeTreeLuaVisitor.Statement
{
    using CodeTree;
    using CsLuaConverter.Context;
    using CsLuaConverter.SyntaxExtensions;

    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    public class ReturnStatementVisitor : BaseVisitor
    {
        //private readonly BaseVisitor innerVisitor;
        public ReturnStatementVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.ExpectKind(0, SyntaxKind.ReturnKeyword);

            if (this.Branch.Nodes.Length == 2)
            {
                this.ExpectKind(1, SyntaxKind.SemicolonToken);
                return;
            }

            this.ExpectKind(2, SyntaxKind.SemicolonToken);
            //this.innerVisitor = this.CreateVisitor(1);
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IContext context)
        {
            var syntax = (ReturnStatementSyntax)this.Branch.SyntaxNode;

            textWriter.Write("return");
            if (syntax.Expression != null)
            {
                textWriter.Write(" ");
                syntax.Expression.Write(textWriter, context);
            }
            
            textWriter.WriteLine(";");
        }
    }
}