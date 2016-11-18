namespace CsLuaConverter.CodeTreeLuaVisitor.Statement.Switch
{
    using System.Linq;
    using CodeTree;
    using CsLuaConverter.Context;
    using Filters;
    using Microsoft.CodeAnalysis.CSharp;

    public class SwitchStatementVisitor : BaseVisitor
    {
        private readonly IVisitor switchTarget;
        private readonly SwitchSectionVisitor[] switchSections;
        public SwitchStatementVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.ExpectKind(0, SyntaxKind.SwitchKeyword);
            this.ExpectKind(1, SyntaxKind.OpenParenToken);
            this.switchTarget = this.CreateVisitor(2);
            this.ExpectKind(3, SyntaxKind.CloseParenToken);
            this.ExpectKind(4, SyntaxKind.OpenBraceToken);
            this.switchSections =
                this.CreateVisitors(new KindRangeFilter(SyntaxKind.OpenBraceToken, SyntaxKind.CloseBraceToken))
                    .Select(v => (SwitchSectionVisitor) v)
                    .ToArray();
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IContext context)
        {
            textWriter.Write("local switchValue = ");
            this.switchTarget.Visit(textWriter, context);
            textWriter.WriteLine(";");
            this.switchSections.VisitAll(textWriter, context, "else");
            textWriter.WriteLine("end");
        }
    }
}