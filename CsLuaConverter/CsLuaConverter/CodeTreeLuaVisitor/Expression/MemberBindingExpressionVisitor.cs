namespace CsLuaConverter.CodeTreeLuaVisitor.Expression
{
    using CsLuaConverter.CodeTree;
    using CsLuaConverter.Context;
    using CsLuaConverter.SyntaxExtensions;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

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
            var syntax = (MemberBindingExpressionSyntax) this.Branch.SyntaxNode;

            textWriter.Write(".");
            syntax.Name.Write(textWriter, context);
        }
    }
}