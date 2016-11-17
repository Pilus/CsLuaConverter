namespace CsLuaConverter.CodeTreeLuaVisitor.Lists
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using CodeTree;
    using Expression.Lambda;
    using Microsoft.CodeAnalysis.CSharp;
    using Providers;

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
            textWriter.Write("(");

            for (var index = 0; index < this.argumentVisitors.Length; index++)
            {
                this.argumentVisitors[index].Visit(textWriter, context);

                if (index < this.argumentVisitors.Length - 1)
                {
                    textWriter.Write(", ");
                }
            }
            textWriter.Write(")");
        }
    }
}