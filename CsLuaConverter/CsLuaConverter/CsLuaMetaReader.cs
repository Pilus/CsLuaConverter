namespace CsLuaConverter
{
    using System.IO;
    using AddOnConstruction;

    internal static class CsLuaMetaReader
    {
        private static string content;

        public static CodeFile GetMetaFile()
        {
            if (content == null)
            {
                var path = new FileInfo(System.Reflection.Assembly.GetEntryAssembly().Location).Directory;
                using (var sr = new StreamReader(path + @"\CsLuaMeta.lua"))
                {
                    content = sr.ReadToEnd();
                }
            }

            return new CodeFile()
            {
                Content = content,
                FileName = "CsLuaMeta.lua",
            };
        }

        public static string GetReferenceString()
        {
            return "local CsLuaMeta = CsLuaMeta;";
        }
    }
}