namespace CsLuaConverter.CodeTreeLuaVisitor.Expression
{
    using CodeTree;
    using CsLuaConverter.Context;
    using CsLuaConverter.SyntaxExtensions;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    public class LogicalNotExpressionVisitor : BaseVisitor
    {
        private readonly IVisitor innnerVisitor;

        public LogicalNotExpressionVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.ExpectKind(0, SyntaxKind.ExclamationToken);
            this.innnerVisitor = this.CreateVisitor(1);
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IContext context)
        {
            var syntax = (PrefixUnaryExpressionSyntax)this.Branch.SyntaxNode;
            textWriter.Write("not(");
            syntax.Operand.Write(textWriter, context);
            textWriter.Write(")");
        }
    }
}