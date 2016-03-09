namespace CsLuaConverter.Providers.TypeProvider
{
    using TypeKnowledgeRegistry;

    public class ScopeElement
    {
        public string ClassPrefix;
        public bool IsFromClass;
        public string Name;
        public TypeKnowledge Type;

        public ScopeElement(string name)
        {
            this.Name = name;
        }

        public ScopeElement(string name, TypeKnowledge type)
        {
            this.Name = name;
            this.Type = type;
        }

        public override string ToString()
        {
            return this.ClassPrefix + this.Name;
        }
    }
}