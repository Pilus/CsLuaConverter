namespace CsLuaSyntaxTranslator
{
    using System.CodeDom.Compiler;
    using System.IO;

    public class IndentedTextWriterWrapper : IIndentedTextWriterWrapper
    {
        private readonly IndentedTextWriter writer;

        public IndentedTextWriterWrapper(StringWriter innerWriter)
        {
            this.writer = new IndentedTextWriter(innerWriter);
        }

        public IndentedTextWriterWrapper(TextWriter innerWriter)
        {
            this.writer = new IndentedTextWriter(innerWriter);
        }

        public int Indent
        {
            get { return this.writer.Indent; }
            set { this.writer.Indent = value; }
        }

        public override string ToString()
        {
            return this.writer.InnerWriter.ToString();
        }

        public void Write(string value)
        {
            this.writer.Write(value);
        }

        public void Write(string format, params object[] arg)
        {
            this.writer.Write(format, arg);
        }

        public void WriteLine(string value)
        {
            this.writer.WriteLine(value);
        }

        public void WriteLine(string format, params object[] arg)
        {
            this.writer.WriteLine(format, arg);
        }

        public IIndentedTextWriterWrapper CreateTextWriterAtSameIndent()
        {
            return new IndentedTextWriterWrapper(new StringWriter()) {Indent = this.writer.Indent};
        }

        public void AppendTextWriter(IIndentedTextWriterWrapper otherWriter)
        {
            this.Write(otherWriter.ToString());
        }

        public void WriteLine()
        {
            this.writer.WriteLine();
        }
    }
}