namespace CsToLua.SyntaxAnalysis
{
    internal class ScopeElement
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