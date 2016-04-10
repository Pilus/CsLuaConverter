namespace CsLuaConverter.CodeTreeLuaVisitor.Statement.Switch
{
    using System.Linq;
    using CodeTree;
    using Filters;
    using Microsoft.CodeAnalysis.CSharp;
    using Providers;

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

        public override void Visit(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            textWriter.Write("local switchValue = ");
            this.switchTarget.Visit(textWriter, providers);
            textWriter.WriteLine(";");
            this.switchSections.VisitAll(textWriter, providers, "else");
            textWriter.WriteLine("end");
        }
    }
}