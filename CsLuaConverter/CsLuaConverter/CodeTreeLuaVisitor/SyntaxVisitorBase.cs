namespace CsLuaConverter.CodeTreeLuaVisitor
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using CodeTree;
    using CsLuaConverter.CodeTreeLuaVisitor.Accessor;
    using CsLuaConverter.CodeTreeLuaVisitor.Attribute;
    using CsLuaConverter.Context;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    public abstract class SyntaxVisitorBase<T> : BaseVisitor where T : CSharpSyntaxNode
    {
        protected T Syntax;

        protected SyntaxVisitorBase(T syntax) : base(null)
        {
            this.Syntax = syntax;
        }

        protected SyntaxVisitorBase(CodeTreeBranch branch) : base(branch)
        {
            this.Syntax = (T)branch.SyntaxNode;
        }

        private static BaseVisitor CreateVisitor(CSharpSyntaxNode node)
        {
            switch (node.GetType().Name)
            {
                case nameof(ArgumentSyntax):
                    return new ArgumentVisitor((ArgumentSyntax) node);
                case nameof(BlockSyntax):
                    return new BlockVisitor((BlockSyntax) node);
                case nameof(EqualsValueClauseSyntax):
                    return new EqualsValueClauseVisitor((EqualsValueClauseSyntax) node);
                case nameof(ParameterSyntax):
                    return new ParameterVisitor((ParameterSyntax) node);
                case nameof(TypeParameterSyntax):
                    return new TypeParameterVisitor((TypeParameterSyntax) node);
                case nameof(UsingDirectiveSyntax):
                    return new UsingDirectiveVisitor((UsingDirectiveSyntax) node);
                case nameof(AccessorListSyntax):
                    return new AccessorListVisitor((AccessorListSyntax) node);
                case nameof(AccessorDeclarationSyntax):
                    return new AccessorDeclarationVisitor((AccessorDeclarationSyntax)node);
                case nameof(AttributeListSyntax):
                    return new AttributeListVisitor((AttributeListSyntax)node);
                case nameof(AttributeSyntax):
                    return new AttributeVisitor((AttributeSyntax)node);
            }

            return BaseVisitor.CreateVisitor(new CodeTreeBranch(node, null));
        }

        public static void VisitNode(CSharpSyntaxNode node, IIndentedTextWriterWrapper textWriter, IContext context)
        {
            var visitor = CreateVisitor(node);
            visitor.Visit(textWriter, context);
        }

        [DebuggerNonUserCode]
        public static void VisitAllNodes(IEnumerable<CSharpSyntaxNode> nodes, IIndentedTextWriterWrapper textWriter, IContext context, Action delimiterAction = null)
        {
            var nodesArray = nodes.ToArray();
            for (var index = 0; index < nodesArray.Length; index++)
            {
                var node = nodesArray[index];
                VisitNode(node, textWriter, context);

                if (index != nodesArray.Length - 1)
                {
                    delimiterAction?.Invoke();
                }
            }
        }
    }
}