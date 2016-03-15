namespace CsLuaConverter.CodeTreeLuaVisitor
{
    using System.CodeDom.Compiler;
    using CodeTree;
    using Microsoft.CodeAnalysis.CSharp;
    using Name;
    using Providers;

    public class UsingDirectiveVisitor : BaseVisitor
    {
        public UsingDirectiveVisitor(CodeTreeBranch branch) : base(branch)
        {
        }

        public override void Visit(IndentedTextWriter textWriter, IProviders providers)
        {
            this.ExpectKind(0, SyntaxKind.UsingKeyword);
            this.ExpectKind(1, new[] { SyntaxKind.QualifiedName, SyntaxKind.IdentifierName });
            var nameVisitor = (INameVisitor)this.CreateVisitor(1);
            providers.TypeProvider.AddNamespace(nameVisitor.GetName());
        }
    }
}