namespace CsLuaConverter.CodeTreeLuaVisitor
{
    using CodeTree;
    using Microsoft.CodeAnalysis.CSharp;
    using Name;
    using Providers;

    public class UsingDirectiveVisitor : BaseVisitor
    {
        private readonly INameVisitor nameVisitor;
        public UsingDirectiveVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.ExpectKind(0, SyntaxKind.UsingKeyword);
            this.ExpectKind(1, new[] { SyntaxKind.QualifiedName, SyntaxKind.IdentifierName });
            this.nameVisitor = (INameVisitor)this.CreateVisitor(1);
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            providers.TypeProvider.AddNamespace(this.nameVisitor.GetName());
        }
    }
}