namespace CsLuaConverter.CodeTreeLuaVisitor.Expression
{
    using System;
    using System.Linq;
    using CodeTree;
    using CsLuaConverter.Context;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    using Name;

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

        public override void Visit(IIndentedTextWriterWrapper textWriter, IContext context)
        {
            if (this.targetVisitor == null)
            {
                this.indexVisitor.Visit(textWriter, context);
                return;
            }

            if (!(this.targetVisitor is ThisExpressionVisitor || this.targetVisitor is BaseExpressionVisitor))
            { 
                textWriter.Write("(");
                this.VisitTarget(textWriter, context);
                textWriter.Write(" % _M.DOT).");
            }

            this.indexVisitor.Visit(textWriter, context);
        }

        private void VisitTarget(IIndentedTextWriterWrapper textWriter, IContext context)
        {
            var symbol = context.SemanticModel.GetSymbolInfo(this.Branch.SyntaxNode).Symbol;

            if (symbol == null)
            {
                this.targetVisitor.Visit(textWriter, context);
                return;
            }

            if (symbol is ITypeSymbol)
            {
                context.TypeReferenceWriter.WriteInteractionElementReference((ITypeSymbol)symbol, textWriter);
                return;
            }

            if (!symbol.IsStatic)
            {
                this.targetVisitor.Visit(textWriter, context);
                return;
            }

            context.TypeReferenceWriter.WriteInteractionElementReference(symbol.ContainingType, textWriter);
        }
    }
}