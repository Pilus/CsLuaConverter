namespace CsLuaConverter.CodeTreeLuaVisitor.Expression
{
    using CsLuaConverter.CodeTree;
    using CsLuaConverter.Context;
    using Microsoft.CodeAnalysis.CSharp;

    public class MemberBindingExpressionVisitor : BaseVisitor
    {
        private BaseVisitor innerVisitor;
        public MemberBindingExpressionVisitor(CodeTreeBranch branch)
            : base(branch)
        {
            this.ExpectKind(0, SyntaxKind.DotToken);
            this.innerVisitor = this.CreateVisitor(1);
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IContext context)
        {
            textWriter.Write(".");
            this.innerVisitor.Visit(textWriter, context);
        }
    }
}