namespace CsLuaConverter.CodeTreeLuaVisitor
{
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.Linq;
    using CodeTree;
    using Filters;
    using Microsoft.CodeAnalysis.CSharp;
    using Providers;

    public class CompilationUnitVisitor : BaseVisitor
    {
        public CompilationUnitVisitor(CodeTreeBranch branch) : base(branch)
        {
        }

        public override void Visit(IndentedTextWriter textWriter, IProviders providers)
        {
            providers.TypeProvider.ClearNamespaces();
            
            this.CreateVisitorsAndVisitBranches(textWriter, providers, new KindFilter(SyntaxKind.UsingDirective));
            this.CreateVisitorsAndVisitBranches(textWriter, providers, new KindFilter(SyntaxKind.NamespaceDeclaration));
        }

        public IEnumerable<string> GetNamespace()
        {
            var firstNamespace = this.Branch.Nodes.OfType<CodeTreeBranch>().First(node => IsKind(node, SyntaxKind.NamespaceDeclaration));
            var firstQualifiedName = firstNamespace.Nodes.OfType<CodeTreeBranch>().FirstOrDefault(node => IsKind(node, SyntaxKind.QualifiedName));

            if (firstQualifiedName == null)
            {
                var identifierName = firstNamespace.Nodes.OfType<CodeTreeBranch>().First(node => IsKind(node, SyntaxKind.IdentifierName));
                return new[]
                {
                    identifierName.Nodes.OfType<CodeTreeLeaf>()
                        .Single(node => IsKind(node, SyntaxKind.IdentifierToken))
                        .Text
                };
            }

            return
                firstQualifiedName.Nodes.OfType<CodeTreeBranch>().Where(node => IsKind(node, SyntaxKind.IdentifierName))
                    .Select(node => node.Nodes.Single() as CodeTreeLeaf).Select(l => l.Text).ToArray();
        }

        public string GetTopNamespace()
        {
            return GetNamespace().First();
        }
    }
}