namespace CsLuaConverter.SyntaxAnalysis.V2
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    public abstract class BaseElement
    {
        public static void ExpectSyntax(SyntaxToken token, SyntaxKind kind)
        {
            if (!token.Parent.IsKind(kind))
            {
                throw new Exception("Unexpected token." + Enum.Parse(typeof(SyntaxKind), token.RawKind + ""));
            }
        }

        private static readonly Dictionary<SyntaxKind, Func<BaseElement>> Mappings = new Dictionary
            <SyntaxKind, Func<BaseElement>>()
        {
            { SyntaxKind.NamespaceDeclaration, () => new NamespaceDeclaration() },
            { SyntaxKind.ClassDeclaration, () => new ClassDeclaration() },
            { SyntaxKind.IdentifierName, () => new IdentifierNameElement() }
        };

        public abstract SyntaxToken Analyze(SyntaxToken token);

        public static BaseElement GenerateMatchingElement(SyntaxToken token)
        {
            var mapping = Mappings.FirstOrDefault(m => token.Parent.IsKind(m.Key));
            return mapping.Value();
        }
    }
}