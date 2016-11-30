namespace CsLuaConverter.SyntaxExtensions
{
    using CsLuaConverter.Context;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    public static class ListExtensions
    {
        public static void Write(this ArgumentListSyntax syntax, IIndentedTextWriterWrapper textWriter, IContext context)
        {
            textWriter.Write("(");
            syntax.Arguments.Write(SyntaxNodeExtensions.Write, textWriter, context);
            textWriter.Write(")");
        }
    }
}