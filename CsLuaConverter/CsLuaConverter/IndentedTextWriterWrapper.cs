namespace CsLuaConverter
{
    using System.CodeDom.Compiler;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;

    public class IndentedTextWriterWrapper : IIndentedTextWriterWrapper
    {
        private readonly IndentedTextWriter writer;

        public IndentedTextWriterWrapper(TextWriter innerWriter)
        {
            this.writer = new IndentedTextWriter(innerWriter);
        }

        public int Indent
        {
            get { return this.writer.Indent; }
            set { this.writer.Indent = value; }
        }

        public TextWriter InnerWriter
        {
            get { return this.writer.InnerWriter; }
        }
        public string NewLine
        {
            get { return this.writer.NewLine; }
            set { this.writer.NewLine = value; }
        }

        public void Write(bool value)
        {
            this.writer.Write(value);
            this.DetermineCurrentNumberOfLines();
        }

        public void Write(string value)
        {
            this.writer.Write(value);
            this.DetermineCurrentNumberOfLines();
        }

        public void Write(string format, params object[] arg)
        {
            this.writer.Write(format, arg);
            this.DetermineCurrentNumberOfLines();
        }

        public void WriteLine(bool value)
        {
            this.writer.WriteLine(value);
            this.DetermineCurrentNumberOfLines();
        }

        public void WriteLine(string value)
        {
            this.writer.WriteLine(value);
            this.DetermineCurrentNumberOfLines();
        }

        public void WriteLine(string format, params object[] arg)
        {
            this.writer.WriteLine(format, arg);
            this.DetermineCurrentNumberOfLines();
        }

        public IIndentedTextWriterWrapper CreateTextWriterAtSameIndent()
        {
            return new IndentedTextWriterWrapper(new StringWriter()) {Indent = this.writer.Indent};
        }

        public void AppendTextWriter(IIndentedTextWriterWrapper otherWriter)
        {
            this.Write(otherWriter.InnerWriter.ToString());
            this.DetermineCurrentNumberOfLines();
        }

        public void WriteLine()
        {
            this.writer.WriteLine();
            this.DetermineCurrentNumberOfLines();
        }

        private void DetermineCurrentNumberOfLines()
        {
            if (true || !Debugger.IsAttached)
            {
                return;
            }

            var str = this.writer.InnerWriter.ToString();
            var count = str.Count(c => c == '\n') + 1;

            // Please breakpoint here:
            var dummy = false;
        }
    }
}