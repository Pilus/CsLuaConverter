namespace CsLuaConverter.CodeTreeLuaVisitor.Lists
{
    using System.Linq;
    using CodeTree;
    using CsLuaConverter.Context;
    using Filters;
    using Microsoft.CodeAnalysis.CSharp;

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
            var c = 1;
            foreach (var visitor in this.visitors)
            {
                if (c > 1)
                {
                    textWriter.Write(",");
                }

                textWriter.Write("['");
                visitor.Visit(textWriter, context);
                textWriter.Write("'] = {0}", c);
                c++;
            }
        }

        public int GetNumElements()
        {
            return this.visitors.Length;
        }
    }
}