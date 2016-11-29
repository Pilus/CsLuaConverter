namespace CsLuaConverter.CodeTreeLuaVisitor.Statement.Switch
{
    using System.Linq;
    using CodeTree;
    using CsLuaConverter.Context;
    using CsLuaConverter.SyntaxExtensions;

    using Microsoft.CodeAnalysis.CSharp.Syntax;

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
            var syntax = (SwitchSectionSyntax)this.Branch.SyntaxNode;

            textWriter.Write("if (");
            syntax.Labels.Write(SwitchExtensions.Write, textWriter, context, () => textWriter.Write(" or "));
            textWriter.WriteLine(") then");
            textWriter.Indent++;
            syntax.Statements.Write(StatementExtensions.Write, textWriter, context, null, s => !(s is BreakStatementSyntax));
            //this.bodyElements.VisitAll(textWriter, context);
            textWriter.Indent--;
        }
    }
}