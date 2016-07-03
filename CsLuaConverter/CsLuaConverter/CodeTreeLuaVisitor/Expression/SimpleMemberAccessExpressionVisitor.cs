namespace CsLuaConverter.CodeTreeLuaVisitor.Expression
{
    using CodeTree;
    using Microsoft.CodeAnalysis.CSharp;
    using Name;
    using Providers;

    public class SimpleMemberAccessExpressionVisitor : BaseVisitor
    {
        private readonly IVisitor targetVisitor;
        private readonly INameVisitor indexVisitor;
        public SimpleMemberAccessExpressionVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.ExpectKind(1, SyntaxKind.DotToken);
            this.targetVisitor = this.CreateVisitor(0);
            this.indexVisitor = (INameVisitor)this.CreateVisitor(2);
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {

            providers.TypeKnowledgeRegistry.CurrentType = null;
            textWriter.Write("(");
            this.targetVisitor.Visit(textWriter, providers);
            textWriter.Write("% _M.DOT");

            if (this.targetVisitor is BaseExpressionVisitor)
            {
                textWriter.Write("_LVL(typeObject.Level - 1)");
            }

            textWriter.Write(").");

            this.indexVisitor.Visit(textWriter, providers);
        }
    }
}