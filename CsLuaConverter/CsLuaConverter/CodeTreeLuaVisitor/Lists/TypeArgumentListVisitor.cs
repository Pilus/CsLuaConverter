namespace CsLuaConverter.CodeTreeLuaVisitor.Lists
{
    using System.Linq;
    using CodeTree;
    using CsLuaConverter.Context;
    using Filters;
    using Microsoft.CodeAnalysis.CSharp;

    public class TypeArgumentListVisitor : BaseVisitor, IListVisitor
    {
        private readonly IVisitor[] visitors;

        public TypeArgumentListVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.visitors =
                this.CreateVisitors(new KindRangeFilter(SyntaxKind.LessThanToken, SyntaxKind.GreaterThanToken, SyntaxKind.CommaToken))
                    .Select(v => (IVisitor) v)
                    .ToArray();
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IContext context)
        {
            textWriter.Write("{");
            for (int index = 0; index < this.visitors.Length; index++)
            {
                var visitor = this.visitors[index];
                visitor.Visit(textWriter, context);
                textWriter.Write(".__typeof");

                if (index < this.visitors.Length - 1)
                {
                    textWriter.Write(", ");
                }
            }
            textWriter.Write("}");
        }

        public int GetNumElements()
        {
            return this.visitors.Length;
        }
    }
}