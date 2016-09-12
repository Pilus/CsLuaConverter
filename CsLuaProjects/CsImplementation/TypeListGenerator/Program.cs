
namespace TypeListGenerator
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    class Program
    {
        static void Main(string[] args)
        {
            var outputFilePath = args.Single();
            var writer = File.CreateText(outputFilePath);
            WriteImplementedSystemTypes(writer);
            writer.Close();
        }

        private static void WriteImplementedSystemTypes(TextWriter writer)
        {
            var systemAssembly = Assembly.GetAssembly(typeof(SystemZZZ.SystemException));
            foreach (var type in systemAssembly.GetTypes())
            {
                WriteType(writer, type);
            }
        }

        private static void WriteType(TextWriter writer, Type type)
        {
            writer.WriteLine(type.FullName.Replace("ZZZ", ""));
        }
    }
}
