namespace CsLuaConverter.CodeTreeLuaVisitor.Statement
{
    using System.Linq;
    using CodeTree;
    using CsLuaConverter.Context;
    using Filters;

    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    public class CatchDeclarationVisitor : BaseVisitor
    {
        private readonly IVisitor type;
        private readonly string variableName;
        public CatchDeclarationVisitor(CodeTreeBranch branch) : base(branch)
        {
            var visitors =
                this.CreateVisitors(new KindRangeFilter(SyntaxKind.OpenParenToken, SyntaxKind.CloseParenToken));
            this.type = visitors.SingleOrDefault();
            this.variableName =
                branch.Nodes
                    .OfType<CodeTreeLeaf>()
                    .SingleOrDefault(l => l.Kind == SyntaxKind.IdentifierToken)?
                    .Text;
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IContext context)
        {
            if (this.type != null)
            {
                var symbol = (ITypeSymbol)context.SemanticModel.GetSymbolInfo(((CatchDeclarationSyntax)this.Branch.SyntaxNode).Type).Symbol;
                textWriter.Write("type = ");
                context.TypeReferenceWriter.WriteTypeReference(symbol, textWriter);
                textWriter.WriteLine(",");
            }

            textWriter.WriteLine("func = function({0})", this.variableName ?? string.Empty);
        }
    }
}