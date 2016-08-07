namespace CsLuaConverter.Providers.TypeProvider
{
    using System;
    using System.Diagnostics;
    using TypeKnowledgeRegistry;

    [DebuggerDisplay("ScopeElement: {ClassPrefix}.{Name}")]
    public class ScopeElement
    {
        public string ClassPrefix;
        public bool IsFromClass;
        public string Name;
        public TypeKnowledge Type;

        public ScopeElement(string name, TypeKnowledge type)
        {
            if (type == null)
            {
                throw new Exception($"Cannot create {nameof(ScopeElement)} from a null {nameof(TypeKnowledge)}.");
            }

            this.Name = name;
            this.Type = type;
        }

        public override string ToString()
        {
            switch (this.ClassPrefix)
            {
                case "element":
                    return "(element % _M.DOT_LVL(typeObject.Level))." + this.Name;
                case null:
                    return this.Name;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}