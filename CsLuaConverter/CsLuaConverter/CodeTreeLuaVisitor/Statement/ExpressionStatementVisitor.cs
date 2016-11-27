namespace CsLuaConverter.CodeTreeLuaVisitor.Statement
{
    using CodeTree;
    using CsLuaConverter.Context;
    using CsLuaConverter.SyntaxExtensions;

    using Microsoft.CodeAnalysis.CSharp.Syntax;

    public class ExpressionStatementVisitor : BaseVisitor
    {
        //private readonly BaseVisitor innerVisitor;
        public ExpressionStatementVisitor(CodeTreeBranch branch) : base(branch)
        {
            //this.ExpectKind(1, SyntaxKind.SemicolonToken);
            //this.innerVisitor = this.CreateVisitor(0);
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IContext context)
        {
            var syntax = (ExpressionStatementSyntax)this.Branch.SyntaxNode;
            syntax.Write(textWriter, context);
        }
    }
}