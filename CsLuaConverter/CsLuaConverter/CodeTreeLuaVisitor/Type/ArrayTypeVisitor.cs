namespace CsLuaConverter.CodeTreeLuaVisitor.Type
{
    using System.CodeDom.Compiler;
    using CodeTree;
    using Providers;
    using Providers.TypeKnowledgeRegistry;

    public class ArrayTypeVisitor : BaseTypeVisitor
    {
        public ArrayTypeVisitor(CodeTreeBranch branch) : base(branch)
        {
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            throw new System.NotImplementedException();
        }

        public override void WriteAsReference(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            throw new System.NotImplementedException();
        }

        public override TypeKnowledge GetType(IProviders providers)
        {
            throw new System.NotImplementedException();
        }
    }
}