namespace CsLuaConverter.CodeTreeLuaVisitor.Statement.Switch
{
    using System.Linq;
    using CodeTree;
    using CsLuaConverter.Context;

    public class SwitchSectionVisitor : BaseVisitor
    {
        private readonly ISwitchLabelVisitor[] labels;
        private readonly BaseVisitor[] bodyElements;

        public SwitchSectionVisitor(CodeTreeBranch branch) : base(branch)
        {
            var visitors = this.CreateVisitors();
            this.labels = visitors.OfType<ISwitchLabelVisitor>().ToArray();
            this.bodyElements = visitors.Where(v => !(v is ISwitchLabelVisitor || v is BreakStatementVisitor)).ToArray();
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IContext context)
        {
            textWriter.Write("if (");
            this.labels.VisitAll(textWriter, context, " or ");
            textWriter.WriteLine(") then");
            textWriter.Indent++;
            this.bodyElements.VisitAll(textWriter, context);
            textWriter.Indent--;
        }
    }
}