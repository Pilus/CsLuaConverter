namespace CsLuaConverter.CodeTreeLuaVisitor
{
    using System.CodeDom.Compiler;
    using System.Linq;
    using CodeTree;
    using Microsoft.CodeAnalysis.CSharp;
    using Providers;

    public class CompilationUnitVisitor : BaseVisitor
    {
        public CompilationUnitVisitor(CodeTreeBranch branch) : base(branch)
        {
        }

        public override void Visit(IndentedTextWriter textWriter, IProviders providers)
        {
            throw new System.NotImplementedException();
        }

        public string GetTopNamespace()
        {
            var firstNamespace =    this.Branch.Nodes.OfType<CodeTreeBranch>().First(node => IsKind(node, SyntaxKind.NamespaceDeclaration));
            var firstQualifiedName = firstNamespace.Nodes.OfType<CodeTreeBranch>().FirstOrDefault(node => IsKind(node, SyntaxKind.QualifiedName));

            if (firstQualifiedName == null)
            {
                firstQualifiedName = firstNamespace;
            }

            var firstIdentifierName = firstQualifiedName.Nodes.OfType<CodeTreeBranch>().First(node => IsKind(node, SyntaxKind.IdentifierName));
            return firstIdentifierName.Nodes.OfType<CodeTreeLeaf>().First(node => IsKind(node, SyntaxKind.IdentifierToken)).Text;
        }
    }
}