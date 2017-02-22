namespace CsLuaConverter.CodeTreeLuaVisitor
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using CodeTree;
    using CsLuaConverter.CodeTreeLuaVisitor.Attribute;
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
    }
}