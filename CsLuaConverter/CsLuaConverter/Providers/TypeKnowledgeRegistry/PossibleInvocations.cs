namespace CsLuaConverter.Providers.TypeKnowledgeRegistry
{
    public class PossibleInvocations
    {
        public TypeKnowledge[] InvocationTypes { get; set; }
        public TypeKnowledge[] InvocationTypesWithAppliedGenerics { get; set; }
        public IIndentedTextWriterWrapper MethodGenericName { get; set; }
        public TypeKnowledge SelectedType { get; set; }
    }
}