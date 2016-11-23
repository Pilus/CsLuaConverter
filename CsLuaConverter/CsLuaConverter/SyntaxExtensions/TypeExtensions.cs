namespace CsLuaConverter.SyntaxExtensions
{
    using System;
    using CsLuaConverter.CodeTreeLuaVisitor;
    using CsLuaConverter.Context;

    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    public static class TypeExtensions
    {
        private static readonly TypeSwitch TypeSwitch = new TypeSwitch(
            (syntax, textWriter, context) =>
                {
                    SyntaxVisitorBase<CSharpSyntaxNode>.VisitNode((CSharpSyntaxNode)syntax, textWriter, context);
                    //throw new Exception($"Could not find extension method for typeSyntax {syntax.GetType().Name}.");
                })
            .Case<IdentifierNameSyntax>(Write);

        /*
        AliasQualifiedNameSyntax
        QualifiedNameSyntax
        SimpleNameSyntax
            GenericNameSyntax
            IdentifierNameSyntax
        */

        public static void Write(this TypeSyntax syntax, IIndentedTextWriterWrapper textWriter, IContext context)
        {
            TypeSwitch.Write(syntax, textWriter, context);
        }

        public static void Write(this IdentifierNameSyntax syntax, IIndentedTextWriterWrapper textWriter, IContext context)
        {
            var symbol = ModelExtensions.GetSymbolInfo(context.SemanticModel, syntax).Symbol;

            var classNode = GetClassDeclarionSyntax(syntax);
            var classSymbol = (ITypeSymbol)context.SemanticModel.GetDeclaredSymbol(classNode);

            var previousToken = syntax.GetFirstToken().GetPreviousToken();
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
                textWriter.Write(syntax.Identifier.Text);
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
    }
}