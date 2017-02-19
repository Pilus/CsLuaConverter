namespace CsLuaConverter.CodeTreeLuaVisitor.Lists
{
    using System.Linq;
    using CodeTree;
    using CsLuaConverter.Context;
    using CsLuaConverter.SyntaxExtensions;
    using Filters;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    public class TypeParameterListVisitor : BaseVisitor, IListVisitor
    {
        private readonly TypeParameterVisitor[] visitors;
        public TypeParameterListVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.ExpectKind(0, SyntaxKind.LessThanToken);
            this.visitors = this.CreateVisitors(new KindFilter(SyntaxKind.TypeParameter)).Select(v => (TypeParameterVisitor)v).ToArray();
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IContext context)
        {
            var syntax = (TypeParameterListSyntax)this.Branch.SyntaxNode;

            syntax.Write(textWriter, context);
        }

        public int GetNumElements()
        {
            return this.visitors.Length;
        }
    }
}