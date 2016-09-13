namespace CsLuaConverter.Providers.TypeProvider.TypeCollections
{
    using System;
    using System.IO;
    using System.Linq;

    public class TypesFileTypeCollection : BaseTypeCollection
    {
        public TypesFileTypeCollection(string filePath)
        {
            var lines = File.ReadAllLines(filePath);
            this.AddRange(lines.Select(l => Type.GetType(l, true)));
        }

        public static TypesFileTypeCollection[] LoadFromCurrentDir()
        {
            return
                Directory.GetFiles(Directory.GetCurrentDirectory(), "*.types")
                    .Select(path => new TypesFileTypeCollection(path))
                    .ToArray();
        }
    }
}