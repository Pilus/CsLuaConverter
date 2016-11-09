namespace CsLuaConverter.CodeTreeLuaVisitor.Name
{
    using System.Linq;
    using CodeTree;
    using CsLuaConverter.CodeTreeLuaVisitor.Expression;
    using Lists;

    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;
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
            var hasInvocationExpressionParent = this.Branch.SyntaxNode.Ancestors().OfType<InvocationExpressionSyntax>().Any();
            if (hasInvocationExpressionParent)
            {
                textWriter.Write(this.name);
                return;
            }

            textWriter.Write(this.name);
            textWriter.Write("[");
            this.argumentListVisitor.Visit(textWriter, providers);
            textWriter.Write("]");
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