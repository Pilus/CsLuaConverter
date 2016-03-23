namespace CsLuaConverter.CodeTreeLuaVisitor.Type
{
    using System.CodeDom.Compiler;
    using CodeTree;
    using Providers;
    using Providers.TypeKnowledgeRegistry;

    public abstract class BaseTypeVisitor : BaseVisitor, ITypeVisitor
    {
        protected BaseTypeVisitor(CodeTreeBranch branch) : base(branch)
        {
        }


        public virtual void WriteAsType(IndentedTextWriter textWriter, IProviders providers)
        {
            this.WriteAsReference(textWriter, providers);
            textWriter.Write(".__typeof");
        }
        public abstract void WriteAsReference(IndentedTextWriter textWriter, IProviders providers);
        public abstract TypeKnowledge GetType(IProviders providers);
    }
}