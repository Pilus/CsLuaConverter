namespace CsLuaConverter.Providers.TypeKnowledgeRegistry
{
    public interface IContext
    {
        TypeKnowledge CurrentType { get; set; }
        TypeKnowledge ExpectedType { get; set; }
        PossibleMethods PossibleMethods { get; set; }
        string[] NamespaceReference { get; set; }
    }
}