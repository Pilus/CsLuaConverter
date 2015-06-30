namespace CsLuaCompiler
{
    using System.IO;

    internal class CodeFile
    {
        public string Content;
        public string FileName;
        public string Header;
        public bool Ignore;

        public void DeployFile(string targetPath)
        {
            string content = this.Content;
            if (!string.IsNullOrEmpty(this.Header))
            {
                content = this.Header + "\r\n" + this.Content;
            }
            File.WriteAllText(Path.Combine(targetPath, this.FileName), content);
        }
    }
}