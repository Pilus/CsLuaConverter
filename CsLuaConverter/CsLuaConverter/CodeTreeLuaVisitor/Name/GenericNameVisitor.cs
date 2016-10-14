namespace CsLuaConverter.CodeTreeLuaVisitor.Name
{
    using System.Linq;
    using CodeTree;
    using Lists;

    using Microsoft.CodeAnalysis;
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
            var current = providers.Context.CurrentType;

            if (current == null)
            {
                // TODO: Handle case where this.name refers to a method on the current element.

                var type = providers.TypeProvider.LookupType(this.name);
                textWriter.Write(type.FullNameWithoutGenerics);

                this.WriteGenericTypes(textWriter, providers);
                providers.Context.CurrentType = this.argumentListVisitor.ApplyGenericsToType(providers, new TypeKnowledge(type.TypeObject));
            }
            else
            {
                providers.Context.PossibleMethods = new PossibleMethods(current.GetTypeKnowledgeForSubElement(this.name, providers).OfType<MethodKnowledge>().ToArray());
                providers.Context.PossibleMethods.FilterOnNumberOfGenerics(this.argumentListVisitor.GetNumElements());
                textWriter.Write(this.name);
                providers.Context.PossibleMethods.WriteMethodGenerics = ((tw) => this.WriteGenericTypes(tw, providers));
                providers.Context.PossibleMethods.MethodGenerics = this.argumentListVisitor.GetTypes(providers);
            }
        }

        public string[] GetName()
        {
            return new[] {this.name};
        }

        public override void WriteAsReference(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            var type = (INamedTypeSymbol)providers.SemanticModel.GetSymbolInfo(this.Branch.SyntaxNode).Symbol;
            //var type = providers.TypeProvider.LookupType(new[] {this.name}, this.argumentListVisitor.GetNumElements());
            
            this.WriteAsReference(textWriter, type);
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

        public void WriteAsReference(IIndentedTextWriterWrapper textWriter, INamedTypeSymbol type)
        {
            textWriter.Write(type.ContainingNamespace + "." + type.Name);

            if (type.TypeArguments.Length > 0)
            {
                textWriter.Write("[{");
                var first = true;
                foreach (var typeArgument in type.TypeArguments)
                {
                    if (!first)
                    {
                        textWriter.Write(", ");
                    }

                    WriteAsReference(textWriter, typeArgument as INamedTypeSymbol);

                    first = false;
                }
                textWriter.Write("}]");
            }

            textWriter.Write(".__typeof");
        }
    }
}