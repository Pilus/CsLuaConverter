namespace CsLuaSyntaxTranslator.SyntaxExtensions
{
    using System;
    using System.Linq;
    using CsLuaSyntaxTranslator.Context;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    public static class TypeExtensions
    {
        private static readonly TypeSwitch TypeSwitch = new TypeSwitch(
            (syntax, textWriter, context) =>
                {
                    //SyntaxVisitorBase<CSharpSyntaxNode>.VisitNode((CSharpSyntaxNode)syntax, textWriter, context);
                    throw new Exception($"Could not find extension method for typeSyntax {syntax.GetType().Name}.");
                })
            .Case<ArrayTypeSyntax>(Write)
            .Case<NullableTypeSyntax>(Write)
            .Case<PredefinedTypeSyntax>(Write)
            .Case<NameSyntax>(NameExtensions.Write);

        /* TypeSyntax:
     x  ArrayTypeSyntax
     x  NameSyntax (see NameExtensions.cs)
     x  NullableTypeSyntax
        OmittedTypeArgumentSyntax
        PointerTypeSyntax
     x  PredefinedTypeSyntax

        
        */

        public static void Write(this TypeSyntax syntax, IIndentedTextWriterWrapper textWriter, IContext context)
        {
            TypeSwitch.Write(syntax, textWriter, context);
        }

        public static void Write(ArrayTypeSyntax syntax, IIndentedTextWriterWrapper textWriter, IContext context)
        {
            var symbol = (ITypeSymbol) context.SemanticModel.GetSymbolInfo(syntax.ElementType).Symbol;
            textWriter.Write("System.Array[{");
            context.TypeReferenceWriter.WriteTypeReference(symbol, textWriter);
            textWriter.Write("}]");
            syntax.RankSpecifiers.Single().Write(textWriter, context);
        }

        public static void Write(NullableTypeSyntax syntax, IIndentedTextWriterWrapper textWriter, IContext context)
        {
            syntax.ElementType.Write(textWriter, context);
        }

        public static void Write(PredefinedTypeSyntax syntax, IIndentedTextWriterWrapper textWriter, IContext context)
        {
            var symbol = (ITypeSymbol)context.SemanticModel.GetSymbolInfo(syntax).Symbol;
            context.TypeReferenceWriter.WriteInteractionElementReference(symbol, textWriter);
        }
    }
}