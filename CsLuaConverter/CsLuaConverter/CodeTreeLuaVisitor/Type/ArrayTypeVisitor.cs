namespace CsLuaConverter.CodeTreeLuaVisitor.Type
{
    using System.Linq;

    using CodeTree;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    using Providers;
    using Providers.TypeKnowledgeRegistry;

    public class ArrayTypeVisitor : BaseTypeVisitor
    {
        private readonly ITypeVisitor type;

        private readonly ArrayRankSpecifierVisitor arrayRank;
        public ArrayTypeVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.ExpectKind(1, SyntaxKind.ArrayRankSpecifier);
            this.type = (ITypeVisitor) this.CreateVisitor(0);
            this.arrayRank = (ArrayRankSpecifierVisitor)this.CreateVisitor(1);
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            var symbol = (ITypeSymbol)providers.SemanticModel.GetSymbolInfo(this.Branch.SyntaxNode.ChildNodes().First()).Symbol;
            textWriter.Write("System.Array[{");
            providers.TypeReferenceWriter.WriteTypeReference(symbol, textWriter);
            textWriter.Write("}]");
            this.arrayRank.Visit(textWriter, providers);
        }

        public override void WriteAsReference(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            textWriter.Write("System.Array[{");
            this.type.WriteAsType(textWriter, providers);
            textWriter.Write("}]"); 
        }

        public override TypeKnowledge GetType(IProviders providers)
        {
            return this.type.GetType(providers).GetAsArrayType();
        }
    }
}