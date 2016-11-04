namespace CsLuaConverter.CodeTreeLuaVisitor.Name
{
    using System.Linq;
    using CodeTree;

    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    using Providers;
    using Providers.GenericsRegistry;
    using Providers.TypeKnowledgeRegistry;
    using Type;

    public class IdentifierNameVisitor : BaseTypeVisitor, INameVisitor
    {
        private readonly string text;

        public IdentifierNameVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.text = ((CodeTreeLeaf)this.Branch.Nodes.Single()).Text;
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            var symbol = providers.SemanticModel.GetSymbolInfo(this.Branch.SyntaxNode).Symbol;

            var classNode = GetClassDeclarionSyntax(this.Branch.SyntaxNode);
            var classSymbol = (ITypeSymbol)providers.SemanticModel.GetDeclaredSymbol(classNode);

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
                providers.TypeReferenceWriter.WriteInteractionElementReference((ITypeSymbol)symbol, textWriter);
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
        /*
        private static ITypeSymbol GetDeclaringSymbol(ISymbol symbol)
        {
            var propertySymbol = symbol as IPropertySymbol;
            var fieldSymbol = symbol as IFieldSymbol;
            var methodSymbol = symbol as IMethodSymbol;
            return 
                symbol?.ContainingType ?? 
                fieldSymbol?.ContainingType ??
                methodSymbol?.ContainingType;
        }*/

        public new void WriteAsType(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            this.WriteAsReference(textWriter, providers);

            if (!providers.GenericsRegistry.IsGeneric(this.text))
            {
                textWriter.Write(".__typeof");
            }
        }

        public override TypeKnowledge GetType(IProviders providers)
        {
            if (providers.GenericsRegistry.IsGeneric(this.text))
            {
                var genericType = providers.GenericsRegistry.GetGenericTypeObject(this.text);
                return new TypeKnowledge(genericType); // TODO: use other type if there are a generic 
            }

            if (this.text == "var")
            {
                return null;
            }

            var type = providers.TypeProvider.LookupType(this.text);
            return type != null ? new TypeKnowledge(type.TypeObject) : null;
        }

        public override void WriteAsReference(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            if (providers.GenericsRegistry.IsGeneric(this.text))
            {
                var scope = providers.GenericsRegistry.GetGenericScope(this.text);
                if (scope.Equals(GenericScope.Class))
                {
                    textWriter.Write("generics[genericsMapping['{0}']]", this.text);
                }
                else
                {
                    textWriter.Write("methodGenerics[methodGenericsMapping['{0}']]", this.text);
                }

                return;
            }

            var type = providers.TypeProvider.LookupType(this.text);
            textWriter.Write(type.FullNameWithoutGenerics);
        }

        public string[] GetName()
        {
            return new[] { this.text};
        }
    }
}