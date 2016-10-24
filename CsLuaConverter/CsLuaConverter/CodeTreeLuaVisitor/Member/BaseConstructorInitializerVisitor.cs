namespace CsLuaConverter.CodeTreeLuaVisitor.Member
{
    using System.Linq;

    using CodeTree;
    using Expression;
    using Lists;

    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    using Providers;
    using Providers.TypeKnowledgeRegistry;

    public class BaseConstructorInitializerVisitor : BaseVisitor
    {
        private readonly ArgumentListVisitor argumentList;
        public BaseConstructorInitializerVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.ExpectKind(0, SyntaxKind.ColonToken);
            this.ExpectKind(1, SyntaxKind.BaseKeyword);
            this.ExpectKind(2, SyntaxKind.ArgumentList);
            this.argumentList = (ArgumentListVisitor) this.CreateVisitor(2);
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            var symbol = (IMethodSymbol)providers.SemanticModel.GetSymbolInfo(this.Branch.SyntaxNode as ConstructorInitializerSyntax).Symbol;
            textWriter.Write("(element % _M.DOT_LVL(typeObject.Level - 1))._C_0_");
            providers.SignatureWriter.WriteSignature(symbol.Parameters.Select(p => p.Type).ToArray(), textWriter);
            this.argumentList.Visit(textWriter, providers);
            textWriter.WriteLine(";");
        }
    }
}