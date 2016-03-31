namespace CsLuaConverter.Providers.TypeKnowledgeRegistry
{
    using CodeElementAnalysis;

    public interface ITypeKnowledgeRegistry
    {
        TypeKnowledge CurrentType { get; set; }
        TypeKnowledge ExpectedType { get; set; }
        TypeKnowledge[] PossibleMethods { get; set; }
    }
}