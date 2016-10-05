namespace CsLuaConverter.CodeTreeLuaVisitor
{
    using System.Linq;
    using CodeTree;
    using Filters;

    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Providers;

    public class CompilationUnitVisitor : BaseVisitor
    {
        private NamespaceDeclarationVisitor namespaceVisitor;
        private UsingDirectiveVisitor[] usings;

        private SemanticModel semanticModel;

        public CompilationUnitVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.semanticModel = branch.SemanticModel;
            TryActionAndWrapException(() =>
            {
                this.namespaceVisitor =
                    (NamespaceDeclarationVisitor)
                        this.CreateVisitors(
                            new KindFilter(SyntaxKind.NamespaceDeclaration)).Single();
                this.usings = this.CreateVisitors(new KindFilter(SyntaxKind.UsingDirective)).Select(v => (UsingDirectiveVisitor)v).ToArray();
            }, $"In document {this.Branch.DocumentName}");
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            providers.Context.SemanticModel = this.semanticModel;
            TryActionAndWrapException(() =>
            {
                providers.TypeProvider.ClearNamespaces();

                this.usings.VisitAll(textWriter, providers);
                this.namespaceVisitor.Visit(textWriter, providers);
            }, $"In document {this.Branch.DocumentName}");
        }

        public void WriteFooter(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            this.namespaceVisitor.WriteFooter(textWriter, providers);
        }

        public string[] GetNamespaceName()
        {
            return this.namespaceVisitor.GetNamespaceName();
        }

        public string GetTopNamespace()
        {
            return this.GetNamespaceName().First();
        }

        public string GetElementName()
        {
            return this.namespaceVisitor.GetElementName();
        }

        public int[] GetNumGenericsOfElement()
        {
            return this.namespaceVisitor.GetNumGenericsOfElement();
        }
    }
}