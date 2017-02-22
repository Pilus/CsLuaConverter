namespace CsLuaSyntaxTranslator.SyntaxExtensions
{
    using CsLuaSyntaxTranslator.Context;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    public static class SwitchExtensions
    {
        public static void Write(this SwitchLabelSyntax syntax, IIndentedTextWriterWrapper textWriter, IContext context)
        {
            var caseSwitch = syntax as CaseSwitchLabelSyntax;
            if (caseSwitch != null)
            {
                caseSwitch.Write(textWriter, context);
                return;
            }

            var defaultSwitch = (DefaultSwitchLabelSyntax)syntax;
            defaultSwitch.Write(textWriter, context);
        }

        public static void Write(this CaseSwitchLabelSyntax syntax, IIndentedTextWriterWrapper textWriter, IContext context)
        {
            textWriter.Write("switchValue == ");
            syntax.Value.Write(textWriter, context);
        }

        public static void Write(this DefaultSwitchLabelSyntax syntax, IIndentedTextWriterWrapper textWriter, IContext context)
        {
            textWriter.Write("true");
        }

        public static void Write(this SwitchSectionSyntax syntax, IIndentedTextWriterWrapper textWriter, IContext context)
        {
            textWriter.Write("if (");
            syntax.Labels.Write(Write, textWriter, context, () => textWriter.Write(" or "));
            textWriter.WriteLine(") then");
            textWriter.Indent++;
            syntax.Statements.Write(StatementExtensions.Write, textWriter, context, null, s => !(s is BreakStatementSyntax));
            textWriter.Indent--;
        }

        public static void Write(this SwitchStatementSyntax syntax, IIndentedTextWriterWrapper textWriter, IContext context)
        {
            textWriter.Write("local switchValue = ");
            syntax.Expression.Write(textWriter, context);
            textWriter.WriteLine(";");
            syntax.Sections.Write(Write, textWriter, context, () => textWriter.Write("else"));
            textWriter.WriteLine("end");
        }
    }
}