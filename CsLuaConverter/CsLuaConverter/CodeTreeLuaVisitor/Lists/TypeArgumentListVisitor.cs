namespace CsLuaConverter.CodeTreeLuaVisitor.Lists
{
    using System.CodeDom.Compiler;
    using System.Linq;
    using CodeTree;
    using Filters;
    using Microsoft.CodeAnalysis.CSharp;
    using Name;
    using Providers;

    public class TypeArgumentListVisitor : BaseVisitor, IListVisitor
    {
        private readonly INameVisitor[] visitors;

        public TypeArgumentListVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.visitors =
                this.CreateVisitors(new KindRangeFilter(SyntaxKind.LessThanToken, SyntaxKind.GreaterThanToken, SyntaxKind.CommaToken))
                    .Select(v => (INameVisitor) v)
                    .ToArray();
        }

        public override void Visit(IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.Write("{");
            foreach (var visitor in this.visitors)
            {
                visitor.WriteAsType(textWriter, providers);
            }
            textWriter.Write("}");
        }

        public int GetNumElements()
        {
            return this.visitors.Length;
        }
    }
}