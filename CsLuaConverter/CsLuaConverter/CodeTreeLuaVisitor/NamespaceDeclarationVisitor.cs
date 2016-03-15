namespace CsLuaConverter.CodeTreeLuaVisitor
{
    using System.CodeDom.Compiler;
    using CodeTree;
    using Filters;
    using Microsoft.CodeAnalysis.CSharp;
    using Name;
    using Providers;

    public class NamespaceDeclarationVisitor : BaseVisitor
    {
        public NamespaceDeclarationVisitor(CodeTreeBranch branch) : base(branch)
        {
        }

        public override void Visit(IndentedTextWriter textWriter, IProviders providers)
        {
            this.ExpectKind(0, SyntaxKind.NamespaceKeyword);
            this.ExpectKind(1, new []{ SyntaxKind.QualifiedName, SyntaxKind.IdentifierName });
            var nameVisitor = (INameVisitor) this.CreateVisitor(1);
            providers.TypeProvider.SetCurrentNamespace(nameVisitor.GetName());

            this.CreateVisitorsAndVisitBranches(textWriter, providers, new KindFilter(SyntaxKind.UsingDirective));

            throw new System.NotImplementedException();
        }
    }
}