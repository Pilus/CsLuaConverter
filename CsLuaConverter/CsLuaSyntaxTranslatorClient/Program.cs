namespace CsLuaSyntaxTranslatorClient
{
    using System.Linq;
    using System.IO;
    using CsLuaSyntaxTranslator;

    class Program
    {
        static void Main(string[] args)
        {
            var projectPath = args[0];
            var outPath = args[1];
            var namespaceConstructor = new NamespaceConstructor();
            var csluaNamespace = namespaceConstructor.GetNamespacesFromProject(projectPath).Single();
            var fileStream = new StreamWriter(outPath);
            var writer = new IndentedTextWriterWrapper(fileStream);
            csluaNamespace.WritingAction(writer);
            fileStream.Close();
        }
    }
}
