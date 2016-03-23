namespace CsLuaConverter.CodeTreeLuaVisitor.Type
{
    using System.CodeDom.Compiler;
    using Providers;
    using Providers.TypeKnowledgeRegistry;

    public interface ITypeVisitor : IVisitor
    {
        void WriteAsType(IndentedTextWriter textWriter, IProviders providers);
        void WriteAsReference(IndentedTextWriter textWriter, IProviders providers);
        TypeKnowledge GetType(IProviders providers);
    }
}