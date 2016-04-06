namespace CsLuaConverter.CodeTreeLuaVisitor.Lists
{
    using System.Linq;
    using CodeTree;
    using Filters;
    using Microsoft.CodeAnalysis.CSharp;
    using Providers;
    using Providers.TypeKnowledgeRegistry;
    using Type;

    public class TypeArgumentListVisitor : BaseVisitor, IListVisitor
    {
        private readonly ITypeVisitor[] visitors;

        public TypeArgumentListVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.visitors =
                this.CreateVisitors(new KindRangeFilter(SyntaxKind.LessThanToken, SyntaxKind.GreaterThanToken, SyntaxKind.CommaToken))
                    .Select(v => (ITypeVisitor) v)
                    .ToArray();
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            textWriter.Write("{");
            for (int index = 0; index < this.visitors.Length; index++)
            {
                var visitor = this.visitors[index];
                visitor.WriteAsType(textWriter, providers);

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

        public TypeKnowledge ApplyGenericsToType(IProviders providers, TypeKnowledge type)
        {
            return type.CreateWithGenerics(this.visitors.Select(v => v.GetType(providers)).ToArray());
        }
    }
}