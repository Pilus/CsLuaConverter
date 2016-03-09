namespace CsLuaConverter.Providers.TypeKnowledgeRegistry
{
    using CodeElementAnalysis;

    public interface ITypeKnowledgeRegistry
    {
        void Add(BaseElement index, TypeKnowledge typeKnowledge);

        TypeKnowledge Get(BaseElement index);
    }
}