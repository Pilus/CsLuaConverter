namespace CsLuaConverter.SyntaxExtensions
{
    using CsLuaConverter.Context;

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
    }
}