namespace CsLuaSyntaxTranslatorClient
{
    using System;
    using System.Linq;
    using System.IO;
    using CsLuaSyntaxTranslator;

    class Program
    {
        static void Main(string[] args)
        {
            try
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
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                Environment.Exit(1);
            }
        }
    }
}
