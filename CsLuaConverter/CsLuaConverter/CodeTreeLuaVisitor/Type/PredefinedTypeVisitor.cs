namespace CsLuaConverter.CodeTreeLuaVisitor.Type
{
    using System.Linq;
    using CodeTree;

    using Providers;
    using Microsoft.CodeAnalysis;
    public class PredefinedTypeVisitor : BaseVisitor
    {
        private readonly string text;

        public PredefinedTypeVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.text = ((CodeTreeLeaf) this.Branch.Nodes.Single()).Text;
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IContext context)
        {
            var symbol = (ITypeSymbol)context.SemanticModel.GetSymbolInfo(this.Branch.SyntaxNode).Symbol;
            context.TypeReferenceWriter.WriteInteractionElementReference(symbol, textWriter);
        }

        public bool IsVoid()
        {
            return this.text == "void";
        }
    }
}