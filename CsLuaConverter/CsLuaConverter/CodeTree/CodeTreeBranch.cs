namespace CsLuaConverter.CodeTree
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    [DebuggerDisplay("CodeTreeBranch - {Kind}")]
    public class CodeTreeBranch : CodeTreeNode
    {
        public CodeTreeNode[] Nodes;
        public string DocumentName;

        public SyntaxNode SyntaxNode;

        public CodeTreeBranch(SyntaxNode node, string documentName)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            this.SyntaxNode = node;
            this.Kind = node.GetKind();
            this.Nodes = this.GetNodes(node);
            this.DocumentName = documentName;
        }

        public CodeTreeBranch(SyntaxNode node, string documentName, CodeTreeNode[] nodes)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            this.SyntaxNode = node;
            this.Kind = node.GetKind();
            this.Nodes = nodes;
            this.DocumentName = documentName;
        }

        public override CodeTreeNode Clone()
        {
            var clonedNodes = this.Nodes.Select(n => n.Clone()).ToArray();
            return new CodeTreeBranch(this.SyntaxNode, this.DocumentName, clonedNodes);
        }

        private CodeTreeNode[] GetNodes(SyntaxNode node)
        {
            var nodes = new List<CodeTreeNode>();
            var token = node.GetFirstToken();
            var lastToken = node.GetLastToken();

            while (token != lastToken.GetNextToken())
            {
                if (token.Parent == node)
                {
                    nodes.Add(new CodeTreeLeaf(token));
                }
                else
                {
                    var subNode = token.GetChildOfAnchestor(node);
                    nodes.Add(new CodeTreeBranch(subNode, this.DocumentName));
                    token = subNode.GetLastToken();
                }

                token = token.GetNextToken();
            }

            return nodes.ToArray();
        }
    }
}