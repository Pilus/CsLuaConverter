namespace CsLuaConverter.CodeTreeLuaVisitor
{
    using System.CodeDom.Compiler;
    using System.IO;
    using CodeElementAnalysis;

    public static class IndentedTextWriterExtension
    {
        public static IndentedTextWriter CreateTextWriterAtSameIndent(this IndentedTextWriter writer)
        {
            var newWriter = new IndentedTextWriter(new StringWriter());
            newWriter.Indent = writer.Indent;
            return newWriter;
        }

        public static void AppendTextWriter(this IndentedTextWriter writer, IndentedTextWriter otherWriter)
        {
            writer.Write(otherWriter.InnerWriter.ToString());
        }
    }
}