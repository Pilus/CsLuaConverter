namespace CsLuaConverter.CodeTreeLuaVisitor.Type
{
    using CodeTree;
    using Microsoft.CodeAnalysis.CSharp;
    using Providers;
    using Providers.TypeKnowledgeRegistry;

    public class ArrayTypeVisitor : BaseTypeVisitor
    {
        private readonly ITypeVisitor type;
        public ArrayTypeVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.ExpectKind(1, SyntaxKind.ArrayRankSpecifier);
            this.type = (ITypeVisitor) this.CreateVisitor(0);
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            this.WriteAsReference(textWriter, providers);
            textWriter.Write("()");
            providers.TypeKnowledgeRegistry.CurrentType = this.GetType(providers);
        }

        public override void WriteAsReference(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            textWriter.Write("System.Array[{");
            this.type.WriteAsType(textWriter, providers);
            textWriter.Write("}]"); 
        }

        public override TypeKnowledge GetType(IProviders providers)
        {
            return this.type.GetType(providers).GetAsArrayType();
        }
    }
}