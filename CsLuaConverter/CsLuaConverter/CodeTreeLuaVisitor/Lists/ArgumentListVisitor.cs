namespace CsLuaConverter.CodeTreeLuaVisitor.Lists
{
    using System.Collections.Generic;

    using CodeTree;
    using CsLuaConverter.Context;
    using CsLuaConverter.SyntaxExtensions;

    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    public class ArgumentListVisitor : BaseVisitor
    {
        private readonly IVisitor[] argumentVisitors;

        public ArgumentListVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.ExpectKind(0, SyntaxKind.OpenParenToken);
            var visitors = new List<IVisitor>();

            for (var i = 1; i < this.Branch.Nodes.Length - 1; i = i + 2)
            {
                visitors.Add(this.CreateVisitor(i));
                this.ExpectKind(i + 1, SyntaxKind.CommaToken, SyntaxKind.CloseParenToken);
            }

            this.argumentVisitors = visitors.ToArray();
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IContext context)
        {
            var syntax = (ArgumentListSyntax)this.Branch.SyntaxNode;

            syntax.Write(textWriter, context);
        }
    }
}