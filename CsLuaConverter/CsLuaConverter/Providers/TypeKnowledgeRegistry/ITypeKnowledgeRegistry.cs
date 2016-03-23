namespace CsLuaConverter.Providers.TypeKnowledgeRegistry
{
    using CodeElementAnalysis;

    public interface ITypeKnowledgeRegistry
    {
        TypeKnowledge CurrentType { get; set; }
    }
}