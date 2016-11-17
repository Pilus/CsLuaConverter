namespace CsLuaConverter.CodeTreeLuaVisitor.Expression
{
    using System;
    using System.Linq;
    using CodeTree;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Providers;
    using Type;

    public class TypeOfExpressionVisitor : BaseVisitor
    {
        public TypeOfExpressionVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.ExpectKind(0, SyntaxKind.TypeOfKeyword);
            this.ExpectKind(1, SyntaxKind.OpenParenToken);
            this.ExpectKind(3, SyntaxKind.CloseParenToken);
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            var symbol = (ITypeSymbol)providers.SemanticModel.GetSymbolInfo(this.Branch.SyntaxNode.ChildNodes().Single()).Symbol;
            providers.TypeReferenceWriter.WriteTypeReference(symbol, textWriter);
        }
    }
}