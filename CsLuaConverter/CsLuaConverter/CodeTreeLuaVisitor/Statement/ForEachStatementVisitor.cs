namespace CsLuaConverter.CodeTreeLuaVisitor.Statement
{
    using CodeTree;
    using Microsoft.CodeAnalysis.CSharp;
    using Providers;
    using Type;

    public class ForEachStatementVisitor : BaseVisitor
    {
        private readonly IVisitor iteratorType;
        private readonly string iteratorName;
        private readonly IVisitor enumerator;
        private readonly BlockVisitor block;

        public ForEachStatementVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.ExpectKind(0, SyntaxKind.ForEachKeyword);
            this.ExpectKind(1, SyntaxKind.OpenParenToken);
            this.iteratorType = this.CreateVisitor(2);
            this.ExpectKind(3, SyntaxKind.IdentifierToken);
            this.iteratorName = ((CodeTreeLeaf) this.Branch.Nodes[3]).Text;
            this.ExpectKind(4, SyntaxKind.InKeyword);
            this.enumerator = this.CreateVisitor(5);
            this.ExpectKind(6, SyntaxKind.CloseParenToken);
            this.ExpectKind(7, SyntaxKind.Block);
            this.block = (BlockVisitor)this.CreateVisitor(7);
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IContext context)
        {
            textWriter.Write("for _,{0} in (", this.iteratorName);
            this.enumerator.Visit(textWriter, context);
            textWriter.WriteLine(" % _M.DOT).GetEnumerator() do");

            this.block.Visit(textWriter, context);
            textWriter.WriteLine("end");
        }
    }
}