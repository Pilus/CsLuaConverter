namespace CsLuaConverter.Providers.TypeKnowledgeRegistry
{
    using Microsoft.CodeAnalysis;

    public interface IContext
    {
        TypeKnowledge CurrentType { get; set; }
        TypeKnowledge ExpectedType { get; set; }
        PossibleMethods PossibleMethods { get; set; }
        string[] NamespaceReference { get; set; }

        SemanticModel SemanticModel { get; set; }
    }
}