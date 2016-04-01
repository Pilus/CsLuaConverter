namespace CsLuaConverter.CodeTreeLuaVisitor.Statement
{
    using System.CodeDom.Compiler;
    using CodeElementAnalysis;
    using CodeTree;
    using Microsoft.CodeAnalysis.CSharp;
    using Providers;

    public class IfStatementVisitor : BaseVisitor
    {
        private readonly IVisitor expression;
        private readonly BlockVisitor block;
        private readonly ElseClauseVisitor elseCause;
        public IfStatementVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.ExpectKind(0, SyntaxKind.IfKeyword);
            this.ExpectKind(1, SyntaxKind.OpenParenToken);
            this.expression = this.CreateVisitor(2);
            this.ExpectKind(3, SyntaxKind.CloseParenToken);
            this.ExpectKind(4, SyntaxKind.Block);
            this.block = (BlockVisitor) this.CreateVisitor(4);

            if (this.IsKind(5, SyntaxKind.ElseClause))
            {
                this.elseCause = (ElseClauseVisitor)this.CreateVisitor(5);
            }
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            textWriter.Write("if (");
            this.expression.Visit(textWriter, providers);
            textWriter.WriteLine(") then");
            this.block.Visit(textWriter, providers);

            if (this.elseCause != null)
            {
                this.elseCause.Visit(textWriter, providers);
            }
            else
            {
                textWriter.WriteLine("end");
            }
        }
    }
}