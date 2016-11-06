namespace CsLuaConverter.CodeTreeLuaVisitor.Expression
{
    using System.Linq;
    using CodeTree;

    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

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

            if (!(this.targetVisitor is ThisExpressionVisitor || this.targetVisitor is BaseExpressionVisitor))
            { 
                textWriter.Write("(");
                this.targetVisitor.Visit(textWriter, providers);
                textWriter.Write(" % _M.DOT).");
            }

            this.indexVisitor.Visit(textWriter, providers);
        }
    }
}