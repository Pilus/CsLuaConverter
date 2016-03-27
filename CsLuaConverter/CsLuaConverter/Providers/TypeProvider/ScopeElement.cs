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
            return (this.ClassPrefix == null ? string.Empty : ($"({this.ClassPrefix} % _M.DOT).")) + this.Name;
        }
    }
}