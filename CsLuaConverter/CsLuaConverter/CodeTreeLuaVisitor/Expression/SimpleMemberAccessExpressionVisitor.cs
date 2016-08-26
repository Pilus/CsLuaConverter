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
            if (this.IsKind(0, SyntaxKind.DotToken))
            {
                this.ExpectKind(0, SyntaxKind.DotToken);
                this.targetVisitor = null;
                this.indexVisitor = (INameVisitor)this.CreateVisitor(1);
            }
            else
            {
                this.ExpectKind(1, SyntaxKind.DotToken);
                this.targetVisitor = this.CreateVisitor(0);
                this.indexVisitor = (INameVisitor)this.CreateVisitor(2);
            }
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            if (this.targetVisitor == null)
            {
                this.indexVisitor.Visit(textWriter, providers);
                return;
            }

            providers.Context.CurrentType = null;

            var innerWriter = textWriter.CreateTextWriterAtSameIndent();

            this.targetVisitor.Visit(innerWriter, providers);

            if (providers.Context.NamespaceReference == null)
            {
                textWriter.Write("(");
            }

            textWriter.AppendTextWriter(innerWriter);

            if (providers.Context.NamespaceReference == null)
            { 
                textWriter.Write("% _M.DOT");

                if (this.targetVisitor is BaseExpressionVisitor)
                {
                    textWriter.Write("_LVL(typeObject.Level - 1, true)");
                }
                else if (this.targetVisitor is ThisExpressionVisitor)
                {
                    textWriter.Write("_LVL(typeObject.Level)");
                }

                textWriter.Write(").");
            }

            this.indexVisitor.Visit(textWriter, providers);
        }
    }
}