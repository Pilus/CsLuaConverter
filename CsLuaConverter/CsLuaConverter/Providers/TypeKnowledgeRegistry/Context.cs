namespace CsLuaConverter.Providers.TypeKnowledgeRegistry
{
    public class Context : IContext
    {
        public TypeKnowledge CurrentType { get; set; }
        public TypeKnowledge ExpectedType { get; set; }
        public PossibleMethods PossibleMethods { get; set; }
        public string[] NamespaceReference { get; set; }
    }
}