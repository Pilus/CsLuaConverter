namespace CsLuaConverter.CodeTreeLuaVisitor.Expression
{
    using System.Linq;

    using CodeTree;

    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Providers;
    using Type;

    public class IsExpressionVisitor : BaseVisitor
    {
        private readonly BaseVisitor target;
        public IsExpressionVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.ExpectKind(1, SyntaxKind.IsKeyword);
            this.target = this.CreateVisitor(0);
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IContext context)
        {
            var symbol = (ITypeSymbol)context.SemanticModel.GetSymbolInfo(this.Branch.SyntaxNode.ChildNodes().Last()).Symbol;
            context.TypeReferenceWriter.WriteInteractionElementReference(symbol, textWriter);
            textWriter.Write(".__is(");
            this.target.Visit(textWriter, context);
            textWriter.Write(")");
        }
    }
}