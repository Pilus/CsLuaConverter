namespace CsLuaConverter.CodeTreeLuaVisitor.Type
{
    using System.Linq;

    using CodeTree;
    using CsLuaConverter.Context;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    public class ArrayTypeVisitor : BaseVisitor
    {
        private readonly ArrayRankSpecifierVisitor arrayRank;
        public ArrayTypeVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.ExpectKind(1, SyntaxKind.ArrayRankSpecifier);
            this.arrayRank = (ArrayRankSpecifierVisitor)this.CreateVisitor(1);
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IContext context)
        {
            var symbol = (ITypeSymbol)context.SemanticModel.GetSymbolInfo(this.Branch.SyntaxNode.ChildNodes().First()).Symbol;
            textWriter.Write("System.Array[{");
            context.TypeReferenceWriter.WriteTypeReference(symbol, textWriter);
            textWriter.Write("}]");
            this.arrayRank.Visit(textWriter, context);
        }
    }
}