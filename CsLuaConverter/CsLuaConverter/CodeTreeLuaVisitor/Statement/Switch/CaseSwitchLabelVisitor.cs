namespace CsLuaConverter.CodeTreeLuaVisitor.Statement.Switch
{
    using CodeTree;
    using CsLuaConverter.Context;
    using Microsoft.CodeAnalysis.CSharp;

    public class CaseSwitchLabelVisitor : BaseVisitor, ISwitchLabelVisitor
    {
        private readonly IVisitor innerVisitor;

        public CaseSwitchLabelVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.ExpectKind(0, SyntaxKind.CaseKeyword);
            this.ExpectKind(2, SyntaxKind.ColonToken);
            this.innerVisitor = this.CreateVisitor(1);
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IContext context)
        {
            textWriter.Write("switchValue == ");
            this.innerVisitor.Visit(textWriter, context);
        }
    }
}