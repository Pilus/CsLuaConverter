namespace CsLuaConverter.CodeTreeLuaVisitor.Expression
{
    using System.Linq;
    using CodeTree;
    using Filters;
    using Microsoft.CodeAnalysis.CSharp;
    using Providers;

    public class CollectionInitializerExpressionVisitor : BaseVisitor
    {
        private readonly BaseVisitor[] innerVisitors;
        public CollectionInitializerExpressionVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.innerVisitors =
                this.CreateVisitors(new KindRangeFilter(SyntaxKind.OpenBraceToken, SyntaxKind.CloseBraceToken,
                    SyntaxKind.CommaToken)).ToArray();
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            textWriter.WriteLine(".__Initialize({");
            textWriter.Indent++;

            providers.TypeKnowledgeRegistry.CurrentType = null;
            this.innerVisitors.VisitAll(textWriter, providers, () =>
            {
                textWriter.WriteLine(",");
                providers.TypeKnowledgeRegistry.CurrentType = null;
            });
            textWriter.Indent--;
            textWriter.WriteLine();
            textWriter.Write("})");
        }
    }
}