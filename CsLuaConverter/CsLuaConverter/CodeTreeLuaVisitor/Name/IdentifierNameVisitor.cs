namespace CsLuaConverter.CodeTreeLuaVisitor.Name
{
    using System.Linq;
    using CodeTree;
    using CsLuaConverter.Context;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;
    using Type;

    public class IdentifierNameVisitor : BaseVisitor, INameVisitor
    {
        private readonly string text;

        public IdentifierNameVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.text = ((CodeTreeLeaf)this.Branch.Nodes.Single()).Text;
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IContext context)
        {
            var symbol = context.SemanticModel.GetSymbolInfo(this.Branch.SyntaxNode).Symbol;

            var classNode = GetClassDeclarionSyntax(this.Branch.SyntaxNode);
            var classSymbol = (ITypeSymbol)context.SemanticModel.GetDeclaredSymbol(classNode);

            var previousToken = this.Branch.SyntaxNode.GetFirstToken().GetPreviousToken();
            var previousPreviousToken = previousToken.GetPreviousToken();

            var isFollowingDot = previousToken.IsKind(SyntaxKind.DotToken);
            var identifierHasThisOrBaseReference =
                isFollowingDot && (
                previousPreviousToken.IsKind(SyntaxKind.ThisKeyword) || 
                previousPreviousToken.IsKind(SyntaxKind.BaseKeyword));

            var isPropertyFieldOrMethod = 
                (symbol.Kind == SymbolKind.Property || symbol.Kind == SymbolKind.Field || symbol.Kind == SymbolKind.Method);

            if ((!isFollowingDot || identifierHasThisOrBaseReference) && isPropertyFieldOrMethod && IsDeclaringTypeThisOrBase(symbol.ContainingType, classSymbol))
            {
                if (previousPreviousToken.IsKind(SyntaxKind.BaseKeyword))
                {
                    textWriter.Write("(element % _M.DOT_LVL(typeObject.Level - 1, true)).");
                }
                else
                {
                    textWriter.Write("(element % _M.DOT_LVL(typeObject.Level)).");
                }
            }

            if (symbol.Kind == SymbolKind.NamedType)
            {
                context.TypeReferenceWriter.WriteInteractionElementReference((ITypeSymbol)symbol, textWriter);
            }
            else
            {
                textWriter.Write(this.text);
            }
        }

        private static SyntaxNode GetClassDeclarionSyntax(SyntaxNode node)
        {
            if (node is ClassDeclarationSyntax)
            {
                return node;
            }

            return GetClassDeclarionSyntax(node.Parent);
        }

        private static bool IsDeclaringTypeThisOrBase(ITypeSymbol declaredType, ITypeSymbol thisSymbol)
        {
            if (Equals(thisSymbol, declaredType))
            {
                return true;
            }

            if (thisSymbol.BaseType == null)
            {
                return false;
            }

            return IsDeclaringTypeThisOrBase(declaredType, thisSymbol.BaseType);
        }

        public string[] GetName()
        {
            return new[] { this.text};
        }
    }
}