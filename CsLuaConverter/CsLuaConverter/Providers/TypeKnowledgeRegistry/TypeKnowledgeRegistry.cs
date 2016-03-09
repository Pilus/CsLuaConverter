namespace CsLuaConverter.Providers.TypeKnowledgeRegistry
{
    using System.Collections.Generic;
    using CodeElementAnalysis;

    public class TypeKnowledgeRegistry : ITypeKnowledgeRegistry
    {
        private Dictionary<BaseElement, TypeKnowledge> dictionary = new Dictionary<BaseElement, TypeKnowledge>();
        public void Add(BaseElement index, TypeKnowledge typeKnowledge)
        {
            this.dictionary.Add(index, typeKnowledge);
        }

        public TypeKnowledge Get(BaseElement index)
        {
            return this.dictionary[index];
        }
    }
}