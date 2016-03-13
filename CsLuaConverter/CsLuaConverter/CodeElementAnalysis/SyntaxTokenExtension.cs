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

        public static bool IsKind(this SyntaxNode node, params SyntaxKind[] kinds)
        {
            var kind = node.GetKind();
            return kinds.Any(k => k.Equals(kind));
        }

        public static bool Is(this SyntaxToken token, SyntaxKind parentKind, SyntaxKind kind)
        {
            return token.GetKind().Equals(kind) && token.Parent.GetKind().Equals(parentKind);
        }

        public static SyntaxNode GetChildOfAnchestor(this SyntaxToken token, SyntaxNode anchestor)
        {
            var node = token.Parent;

            while (node != null && node.Parent != anchestor)
            {
                node = node.Parent;
            }

            return node;
        }
    }
}