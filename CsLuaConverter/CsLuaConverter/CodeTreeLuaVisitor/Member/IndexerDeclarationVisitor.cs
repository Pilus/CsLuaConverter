namespace CsLuaConverter.CodeTreeLuaVisitor.Member
{
    using CodeTree;
    using Microsoft.CodeAnalysis.CSharp;
    using Providers;

    public class IndexerDeclarationVisitor : BaseVisitor
    {
        public IndexerDeclarationVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.ExpectKind(0, SyntaxKind.IdentifierName);
            this.ExpectKind(1, SyntaxKind.ThisKeyword);
            this.ExpectKind(2, SyntaxKind.BracketedParameterList);
            this.ExpectKind(3, SyntaxKind.AccessorList);
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            throw new System.NotImplementedException();
        }
    }
}