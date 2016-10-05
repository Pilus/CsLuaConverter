namespace CsLuaConverter.Providers.TypeKnowledgeRegistry
{
    using Microsoft.CodeAnalysis;

    public class Context : IContext
    {
        public TypeKnowledge CurrentType { get; set; }
        public TypeKnowledge ExpectedType { get; set; }
        public PossibleMethods PossibleMethods { get; set; }
        public string[] NamespaceReference { get; set; }

        public SemanticModel SemanticModel { get; set; }
    }
}