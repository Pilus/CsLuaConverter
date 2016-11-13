namespace CsLuaConverter.CodeTreeLuaVisitor
{
    using System;
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

        private readonly SemanticModel semanticModel;

        public CompilationUnitVisitor(CodeTreeBranch branch, SemanticModel semanticModel) : base(branch)
        {
            this.semanticModel = semanticModel;
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
            TryActionAndWrapException(
                () =>
                    {
                        providers.SemanticModel = this.semanticModel;

                        this.usings.VisitAll(textWriter, providers);
                        this.namespaceVisitor.Visit(textWriter, providers);
                    },
                $"In document {this.Branch.DocumentName}");
        }

        public void WriteFooter(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            this.namespaceVisitor.WriteFooter(textWriter, providers);
        }

        public void WriteExtensions(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            this.namespaceVisitor.WriteExtensions(textWriter, providers);
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