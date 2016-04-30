namespace CsLuaConverter.CodeTree
{
    using System;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    public static class SyntaxExtension
    {
        public static SyntaxKind GetKind(this SyntaxToken token)
        {
            return (SyntaxKind)Enum.Parse(typeof(SyntaxKind), token.RawKind.ToString());
        }
        public static SyntaxKind GetKind(this SyntaxNode node)
        {
            return (SyntaxKind)Enum.Parse(typeof(SyntaxKind), node.RawKind.ToString());
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