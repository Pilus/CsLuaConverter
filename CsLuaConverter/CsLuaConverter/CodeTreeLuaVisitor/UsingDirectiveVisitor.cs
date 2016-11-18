namespace CsLuaConverter.CodeTreeLuaVisitor
{
    using CodeTree;
    using CsLuaConverter.Context;
    using Microsoft.CodeAnalysis.CSharp;
    using Name;

    public class UsingDirectiveVisitor : BaseVisitor
    {
        private readonly IVisitor nameVisitor;
        public UsingDirectiveVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.ExpectKind(0, SyntaxKind.UsingKeyword);
            this.ExpectKind(1, SyntaxKind.QualifiedName, SyntaxKind.IdentifierName);
            this.nameVisitor = this.CreateVisitor(1);
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IContext context)
        {
        }
    }
}