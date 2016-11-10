namespace CsLuaConverter.CodeTreeLuaVisitor.Expression
{
    using System;
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
                this.VisitTarget(textWriter, providers);
                textWriter.Write(" % _M.DOT).");
            }

            this.indexVisitor.Visit(textWriter, providers);
        }

        private void VisitTarget(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            var symbol = providers.SemanticModel.GetSymbolInfo(this.Branch.SyntaxNode).Symbol;

            if (symbol == null)
            {
                this.targetVisitor.Visit(textWriter, providers);
                return;
            }

            if (symbol is ITypeSymbol)
            {
                providers.TypeReferenceWriter.WriteInteractionElementReference((ITypeSymbol)symbol, textWriter);
                return;
            }

            if (!symbol.IsStatic)
            {
                this.targetVisitor.Visit(textWriter, providers);
                return;
            }

            providers.TypeReferenceWriter.WriteInteractionElementReference(symbol.ContainingType, textWriter);
        }
    }
}