namespace CsLuaConverter.CodeTreeLuaVisitor.Type
{
    using CodeTree;

    using Microsoft.CodeAnalysis;

    using Providers;
    using Providers.TypeKnowledgeRegistry;

    public abstract class BaseTypeVisitor : BaseVisitor, ITypeVisitor
    {
        protected BaseTypeVisitor(CodeTreeBranch branch) : base(branch)
        {
        }


        

        public virtual void WriteAsType(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            this.WriteAsReference(textWriter, providers);
            textWriter.Write(".__typeof");
        }
        public abstract void WriteAsReference(IIndentedTextWriterWrapper textWriter, IProviders providers);
        public abstract TypeKnowledge GetType(IProviders providers);
    }
}