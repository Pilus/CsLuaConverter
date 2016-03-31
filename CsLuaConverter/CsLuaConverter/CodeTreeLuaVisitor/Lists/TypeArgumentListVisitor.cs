namespace CsLuaConverter.CodeTreeLuaVisitor.Lists
{
    using System.CodeDom.Compiler;
    using System.Linq;
    using CodeTree;
    using Filters;
    using Microsoft.CodeAnalysis.CSharp;
    using Name;
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

        public TypeKnowledge ApplyGenericsToType(IProviders providers, TypeKnowledge type)
        {
            return type.CreateWithGenerics(this.visitors.Select(v => v.GetType(providers)).ToArray());
        }
    }
}