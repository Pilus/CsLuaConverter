namespace CsLuaConverter.CodeTreeLuaVisitor.Type
{
    using CodeTree;
    using CsLuaConverter.Context;
    using Microsoft.CodeAnalysis.CSharp;

    public class NullableTypeVisitor : BaseVisitor
    {
        private readonly IVisitor innerVisitor;
        public NullableTypeVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.ExpectKind(1, SyntaxKind.QuestionToken);
            this.innerVisitor = this.CreateVisitor(0);
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IContext context)
        {
            this.innerVisitor.Visit(textWriter, context);
        }
    }
}