namespace CsLuaConverter.CodeTreeLuaVisitor.Statement
{
    using System.Linq;
    using CodeTree;
    using Filters;

    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    using Providers;
    using Providers.TypeProvider;
    using Type;

    public class CatchDeclarationVisitor : BaseVisitor
    {
        private readonly ITypeVisitor type;
        private readonly string variableName;
        public CatchDeclarationVisitor(CodeTreeBranch branch) : base(branch)
        {
            var visitors =
                this.CreateVisitors(new KindRangeFilter(SyntaxKind.OpenParenToken, SyntaxKind.CloseParenToken));
            this.type = (ITypeVisitor) visitors.SingleOrDefault();
            this.variableName =
                branch.Nodes
                    .OfType<CodeTreeLeaf>()
                    .SingleOrDefault(l => l.Kind == SyntaxKind.IdentifierToken)?
                    .Text;
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            if (this.type != null)
            {
                var symbol = (ITypeSymbol)providers.SemanticModel.GetSymbolInfo(((CatchDeclarationSyntax)this.Branch.SyntaxNode).Type).Symbol;
                textWriter.Write("type = ");
                providers.TypeReferenceWriter.WriteTypeReference(symbol, textWriter);
                textWriter.WriteLine(",");
            }

            textWriter.WriteLine("func = function({0})", this.variableName ?? string.Empty);
        }
    }
}