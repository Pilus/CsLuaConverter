namespace CsLuaConverter.CodeElementAnalysis
{
    using System;
    using System.Linq;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    public static class SyntaxTokenExtension
    {
        public static SyntaxKind GetKind(this SyntaxToken token)
        {
            return (SyntaxKind)Enum.Parse(typeof(SyntaxKind), token.RawKind.ToString());
        }
        public static SyntaxKind GetKind(this SyntaxNode node)
        {
            return (SyntaxKind)Enum.Parse(typeof(SyntaxKind), node.RawKind.ToString());
        }

        public static bool IsKind(this SyntaxNode node, SyntaxKind[] kinds)
        {
            return kinds.Any(kind => node.IsKind(kind));
        }
    }
}