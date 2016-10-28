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
        private readonly ITypeVisitor type;
        public IsExpressionVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.ExpectKind(1, SyntaxKind.IsKeyword);
            this.target = this.CreateVisitor(0);
            this.type = (ITypeVisitor)this.CreateVisitor(2);
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            //this.type.WriteAsReference(textWriter, providers);
            //
            var symbol = (ITypeSymbol)providers.SemanticModel.GetSymbolInfo(this.Branch.SyntaxNode.ChildNodes().Last()).Symbol;
            providers.TypeReferenceWriter.WriteInteractionElementReference(symbol, textWriter);
            textWriter.Write(".__is(");
            this.target.Visit(textWriter, providers);
            textWriter.Write(")");
        }
    }
}