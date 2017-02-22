namespace CsLuaConverter.CodeTreeLuaVisitor
{
    using System;
    using System.Linq;
    using CodeTree;
    using CsLuaSyntaxTranslator;
    using CsLuaSyntaxTranslator.Context;
    using CsLuaSyntaxTranslator.SyntaxExtensions;
    using Filters;

    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

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

        public override void Visit(IIndentedTextWriterWrapper textWriter, IContext context)
        {
            var syntax = this.Branch.SyntaxNode as CompilationUnitSyntax;
            context.SemanticModel = this.semanticModel;
            //context.DocumentPath = this.Branch.DocumentName;

            syntax.Write(textWriter, context);
        }

        public DocumentContent AsDocumentContent()
        {
            return new DocumentContent(this.Branch.SyntaxNode as CompilationUnitSyntax, this.Branch.DocumentName, this.semanticModel);
        }

        public void WriteFooter(IIndentedTextWriterWrapper textWriter, IContext context)
        {
            this.namespaceVisitor.WriteFooter(textWriter, context);
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