namespace CsLuaConverter.CodeTreeLuaVisitor.Expression
{
    using CodeTree;
    using Lists;
    using Microsoft.CodeAnalysis.CSharp;
    using Providers;

    public class ElementAccessExpressionVisitor : BaseVisitor
    {
        private readonly BaseVisitor target;
        private readonly BracketedArgumentListVisitor bracketedArgumentList;
        public ElementAccessExpressionVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.ExpectKind(1, SyntaxKind.BracketedArgumentList);
            this.target = this.CreateVisitor(0);
            this.bracketedArgumentList = (BracketedArgumentListVisitor)this.CreateVisitor(1);
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            textWriter.Write("(");
            this.target.Visit(textWriter, providers);
            textWriter.Write(" % _M.DOT)");
            this.bracketedArgumentList.Visit(textWriter, providers);
        }
    }
}