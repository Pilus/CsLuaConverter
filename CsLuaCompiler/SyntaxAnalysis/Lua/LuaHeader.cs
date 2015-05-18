namespace CsLuaCompiler.SyntaxAnalysis.Lua
{
    using System.IO;

    public static class LuaHeader
    {
        private static string header;

        public static string GetHeader()
        {
            if (header == null)
            {
                var path = new FileInfo(System.Reflection.Assembly.GetEntryAssembly().Location).Directory;
                using (var sr = new StreamReader(path + @"\SyntaxAnalysis\Lua\Header.lua"))
                {
                    header = sr.ReadToEnd();
                }
            }
            return header;
        }
    }
}