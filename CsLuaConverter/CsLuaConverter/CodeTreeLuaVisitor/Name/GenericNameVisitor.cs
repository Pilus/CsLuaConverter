namespace CsLuaConverter.CodeTreeLuaVisitor.Name
{
    using System.Linq;
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
            var current = providers.TypeKnowledgeRegistry.CurrentType;

            if (current == null)
            {
                // TODO: Handle case where this.name refers to a method on the current element.

                var type = providers.TypeProvider.LookupType(this.name);
                textWriter.Write(type.FullNameWithoutGenerics);

                this.WriteGenericTypes(textWriter, providers);
                providers.TypeKnowledgeRegistry.CurrentType = this.argumentListVisitor.ApplyGenericsToType(providers, new TypeKnowledge(type.TypeObject));
            }
            else
            {
                providers.TypeKnowledgeRegistry.PossibleMethods = new PossibleMethods(current.GetTypeKnowledgeForSubElement(this.name, providers).OfType<MethodKnowledge>().ToArray());
                providers.TypeKnowledgeRegistry.PossibleMethods.FilterOnNumberOfGenerics(this.argumentListVisitor.GetNumElements());
                textWriter.Write(this.name);
                providers.TypeKnowledgeRegistry.PossibleMethods.SetWriteMethodGenericsMethod(() => this.WriteGenericTypes(textWriter, providers));
            }
        }

        public string[] GetName()
        {
            return new[] {this.name};
        }

        public override void WriteAsReference(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            var type = providers.TypeProvider.LookupType(new[] {this.name}, this.argumentListVisitor.GetNumElements());
            textWriter.Write(type.FullNameWithoutGenerics);
            this.WriteGenericTypes(textWriter, providers);
        }

        public override TypeKnowledge GetType(IProviders providers)
        {
            var type = providers.TypeProvider.LookupType(new[] {this.name}, this.argumentListVisitor.GetNumElements());

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