namespace CsLuaCompiler.SyntaxAnalysis.NameAndTypeProvider
{
    public class ScopeElement
    {
        public string ClassPrefix;
        public bool IsFromClass;
        public string Name;

        public ScopeElement(string name)
        {
            this.Name = name;
        }
    }
}