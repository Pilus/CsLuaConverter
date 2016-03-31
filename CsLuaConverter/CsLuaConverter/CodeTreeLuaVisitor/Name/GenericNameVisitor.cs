namespace CsLuaConverter.CodeTreeLuaVisitor.Name
{
    using System;
    using System.CodeDom.Compiler;
    using CodeTree;
    using Lists;
    using Microsoft.CodeAnalysis.CSharp;
    using Providers;
    using Providers.TypeKnowledgeRegistry;
    using Type;

    public class GenericNameVisitor : BaseTypeVisitor, INameVisitor
    {
        private readonly string name;
        private readonly TypeArgumentListVisitor argumentListVisitor;

        public GenericNameVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.ExpectKind(0, SyntaxKind.IdentifierToken);
            this.name = ((CodeTreeLeaf) this.Branch.Nodes[0]).Text;
            this.argumentListVisitor = (TypeArgumentListVisitor) this.CreateVisitor(1);
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            throw new System.NotImplementedException();
        }

        public string[] GetName()
        {
            return new[] {this.name};
        }

        public override void WriteAsReference(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            var type = providers.TypeProvider.LookupType(this.name);
            textWriter.Write(type.FullNameWithoutGenerics);
            this.WriteGenericTypes(textWriter, providers);
        }

        public override TypeKnowledge GetType(IProviders providers)
        {
            var type = providers.TypeProvider.LookupType(this.name);

            return this.argumentListVisitor.ApplyGenericsToType(providers, new TypeKnowledge(type.TypeObject));
        }

        public void WriteGenericTypes(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            textWriter.Write("[");
            this.argumentListVisitor.Visit(textWriter, providers);
            textWriter.Write("]");
        }
    }
}