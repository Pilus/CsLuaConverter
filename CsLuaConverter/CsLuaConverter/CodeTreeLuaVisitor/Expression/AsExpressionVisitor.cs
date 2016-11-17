namespace CsLuaConverter.CodeTreeLuaVisitor.Expression
{
    using CodeTree;
    using CsLuaConverter.Context;
    using Microsoft.CodeAnalysis.CSharp;
    using Type;

    public class AsExpressionVisitor : BaseVisitor
    {
        private readonly IVisitor target;
        private readonly IVisitor type;

        public AsExpressionVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.target = this.CreateVisitor(0);
            this.ExpectKind(1, SyntaxKind.AsKeyword);
            this.type = this.CreateVisitor(2);
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IContext context)
        {
            this.target.Visit(textWriter, context);
        }
    }
}