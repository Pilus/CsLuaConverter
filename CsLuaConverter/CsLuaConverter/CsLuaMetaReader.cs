namespace CsLuaConverter
{
    using System.IO;
    using System.Reflection;
    using AddOnConstruction;

    internal static class CsLuaMetaReader
    {
        private static string content;

        public static CodeFile GetMetaFile()
        {
            if (content == null)
            {
                var path = new FileInfo(Assembly.GetEntryAssembly().Location).Directory;
                using (var sr = new StreamReader(path + @"\CsLua.lua"))
                {
                    content = sr.ReadToEnd();
                }
            }

            return new CodeFile()
            {
                Content = content,
                FileName = "CsLua.lua",
            };
        }

        public static string GetReferenceString()
        {
            return "local _M = _M;";
        }
    }
}