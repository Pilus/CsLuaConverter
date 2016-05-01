namespace CsLuaConverter
{
    using System.IO;

    public interface IIndentedTextWriterWrapper
    {
        int Indent { get; set; }
        TextWriter InnerWriter { get; }
        void Write(string value);
        void Write(string format, params object[] arg);
        void WriteLine(string value);
        void WriteLine();
        void WriteLine(string format, params object[] arg);
        IIndentedTextWriterWrapper CreateTextWriterAtSameIndent();
        void AppendTextWriter(IIndentedTextWriterWrapper otherWriter);
    }
}