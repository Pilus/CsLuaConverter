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
                var type = providers.TypeProvider.LookupType(this.name);
                textWriter.Write(type.FullNameWithoutGenerics);

                var memberWithClassGenerics = this.argumentListVisitor.ApplyGenericsToType(providers, new TypeKnowledge(type.TypeObject));
                var memberWithMethodGenerics = memberWithClassGenerics.ApplyMethodGenerics(this.argumentListVisitor.GetTypes(providers));

                providers.TypeKnowledgeRegistry.CurrentType = memberWithMethodGenerics;
            }
            else
            {
                var possibleMembers = current.GetTypeKnowledgeForSubElement(this.name, providers);
                var numAppliedGenerics = this.argumentListVisitor.GetNumElements();
                var fittingMembers = possibleMembers.Where(m => m.MethodGenerics?.Length == numAppliedGenerics).ToArray();

                if (!fittingMembers.Any())
                {
                    throw new VisitorException("Could not find fitting member.");
                }

                var fittingMembersWithMethodGenericsApplied = fittingMembers.Select(m => m.ApplyMethodGenerics(this.argumentListVisitor.GetTypes(providers))).ToArray();

                if (fittingMembersWithMethodGenericsApplied.Length == 1)
                {
                    providers.TypeKnowledgeRegistry.CurrentType = fittingMembersWithMethodGenericsApplied.Single();
                }
                else
                {
                    providers.TypeKnowledgeRegistry.PossibleMethods = fittingMembersWithMethodGenericsApplied;
                    providers.TypeKnowledgeRegistry.CurrentType = null;
                }
            }

            this.WriteGenericTypes(textWriter, providers);
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