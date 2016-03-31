namespace CsLuaConverter.CodeTreeLuaVisitor.Type
{
    using System.CodeDom.Compiler;
    using Providers;
    using Providers.TypeKnowledgeRegistry;

    public interface ITypeVisitor : IVisitor
    {
        void WriteAsType(IIndentedTextWriterWrapper textWriter, IProviders providers);
        void WriteAsReference(IIndentedTextWriterWrapper textWriter, IProviders providers);
        TypeKnowledge GetType(IProviders providers);
    }
}