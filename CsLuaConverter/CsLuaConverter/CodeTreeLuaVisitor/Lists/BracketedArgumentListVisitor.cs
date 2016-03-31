namespace CsLuaConverter.CodeTreeLuaVisitor.Lists
{
    using System.CodeDom.Compiler;
    using System.Linq;
    using CodeTree;
    using Filters;
    using Microsoft.CodeAnalysis.CSharp;
    using Providers;

    public class BracketedArgumentListVisitor : BaseVisitor
    {
        private readonly ArgumentVisitor argument;
        public BracketedArgumentListVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.argument = (ArgumentVisitor) this.CreateVisitors(new KindFilter(SyntaxKind.Argument)).Single();
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            var type = providers.TypeKnowledgeRegistry.CurrentType;
            providers.TypeKnowledgeRegistry.CurrentType = null;
            textWriter.Write("[");
            this.argument.Visit(textWriter, providers);
            textWriter.Write("]");

            providers.TypeKnowledgeRegistry.CurrentType = type.GetIndexerValueType();
        }
    }
}