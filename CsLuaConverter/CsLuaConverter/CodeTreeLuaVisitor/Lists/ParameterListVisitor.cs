namespace CsLuaConverter.CodeTreeLuaVisitor.Lists
{
    using System.Linq;
    using CodeTree;
    using CsLuaConverter.Context;
    using CsLuaConverter.SyntaxExtensions;
    using Filters;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    public class ParameterListVisitor : BaseVisitor
    {
        private readonly ParameterVisitor[] parameters;

        public string FirstElementPrefix = string.Empty;

        public ParameterListVisitor(CodeTreeBranch branch) : base(branch)
        {
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IContext context)
        {
            var syntax = (ParameterListSyntax)this.Branch.SyntaxNode;

            ListExtensions.Write(syntax, textWriter, context);
        }
    }
}