namespace CsLuaCompiler
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using CsLuaAttributes;

    class TocBuilder
    {
        private readonly IList<CodeFile> codeFiles;
        private readonly CsLuaAddOnAttribute attribute;

        public TocBuilder(IList<CodeFile> codeFiles, CsLuaAddOnAttribute attribute)
        {
            this.codeFiles = codeFiles;
            this.attribute = attribute;
        }

        public string Build()
        {
            var sw = new StringWriter();

            this.WriteAttributes(sw);
            this.WriteFiles(sw);

            return sw.ToString();
        }

        private void WriteAttributes(TextWriter sw)
        {
            WriteTag(sw, "Interface", this.attribute.InterfaceVersion);
            WriteTag(sw, "Title", this.attribute.Title);
            WriteTag(sw, "Notes", this.attribute.Notes);
            WriteTag(sw, "Dependencies", this.attribute.Dependencies);
            WriteTag(sw, "OptionalDeps", this.attribute.OptionalDeps);
            WriteTag(sw, "LoadOnDemand", this.attribute.LoadOnDemand);
            WriteTag(sw, "LoadWith", this.attribute.LoadWith);
            WriteTag(sw, "LoadManagers", this.attribute.LoadManagers);
            WriteTag(sw, "SavedVariables", this.attribute.SavedVariables);
            WriteTag(sw, "SavedVariablesPerCharacter", this.attribute.SavedVariablesPerCharacter);
            WriteTag(sw, "DefaultState", this.attribute.DefaultState);
            WriteTag(sw, "Author", this.attribute.Author);
            WriteTag(sw, "Version", this.attribute.Version);
        }

        private void WriteFiles(TextWriter sw)
        {
            foreach (var codeFile in this.codeFiles)
            {
                sw.WriteLine(codeFile.FileName);
            }
        }

        private static void WriteTag(TextWriter sw, string tag, string[] value)
        {
            if (value == null) return;
            WriteTag(sw, tag, string.Join(",", value));
        }

        private static void WriteTag(TextWriter sw, string tag, object value)
        {
            if (value == null) return;
            WriteTag(sw, tag, value.ToString());
        }

        private static void WriteTag(TextWriter sw, string tag, string value)
        {
            if (value == null) return;
            sw.WriteLine("## {0}: {1}", tag, value);
        }
    }
}