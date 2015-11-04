namespace CsLuaConverter.SyntaxAnalysis.V2
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    public abstract class BaseElement
    {

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