namespace CsLuaSyntaxTranslator.SyntaxExtensions
{
    using System.Linq;
    using CsLuaSyntaxTranslator.Context;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    public static class LoopExtensions
    {
        public static void Write(this ForEachStatementSyntax syntax, IIndentedTextWriterWrapper textWriter, IContext context)
        {
            textWriter.Write("for _,{0} in (", syntax.Identifier.Text);
            syntax.Expression.Write(textWriter, context);
            textWriter.WriteLine(" % _M.DOT).GetEnumerator() do");

            syntax.Statement.Write(textWriter, context);
            textWriter.WriteLine("end");
        }

        public static void Write(this ForStatementSyntax syntax, IIndentedTextWriterWrapper textWriter, IContext context)
        {
            syntax.Declaration.Write(textWriter, context);
            textWriter.WriteLine(";");
            textWriter.Write("while (");
            syntax.Condition.Write(textWriter, context);
            textWriter.WriteLine(") do");

            syntax.Statement.Write(textWriter, context);
            syntax.Incrementors.Single().Write(textWriter, context);

            textWriter.WriteLine(";");
            textWriter.WriteLine("end");
        }

        public static void Write(this WhileStatementSyntax syntax, IIndentedTextWriterWrapper textWriter, IContext context)
        {
            textWriter.Write("while (");
            syntax.Condition.Write(textWriter, context);
            textWriter.WriteLine(") do");
            syntax.Statement.Write(textWriter, context);
            textWriter.WriteLine("end");
        }
    }
}