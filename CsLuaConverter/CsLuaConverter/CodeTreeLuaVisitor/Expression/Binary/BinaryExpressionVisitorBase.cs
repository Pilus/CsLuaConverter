namespace CsLuaConverter.CodeTreeLuaVisitor.Expression.Binary
{
    using CodeTree;
    using Microsoft.CodeAnalysis.CSharp;
    using Providers;
    using Providers.TypeKnowledgeRegistry;

    public abstract class BinaryExpressionVisitorBase : BaseVisitor
    {
        private readonly IVisitor lhsVisitor;
        private readonly IVisitor rhsVisitor;
        private readonly string token;

        protected BinaryExpressionVisitorBase(CodeTreeBranch branch, SyntaxKind expectedTokenKind, string alternativeText = null) : base(branch)
        {
            this.ExpectKind(1, expectedTokenKind);
            this.token = alternativeText ?? ((CodeTreeLeaf) this.Branch.Nodes[1]).Text;
            this.lhsVisitor = this.CreateVisitor(0);
            this.rhsVisitor = this.CreateVisitor(2);
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            providers.TypeKnowledgeRegistry.CurrentType = null;
            this.lhsVisitor.Visit(textWriter, providers);
            var lhsType = providers.TypeKnowledgeRegistry.CurrentType;

            textWriter.Write($" {this.token} ");
            providers.TypeKnowledgeRegistry.CurrentType = null;
            this.rhsVisitor.Visit(textWriter, providers);
            var rhsType = providers.TypeKnowledgeRegistry.CurrentType;

            providers.TypeKnowledgeRegistry.CurrentType = DetermineResultingType(lhsType, rhsType);
        }

        private static TypeKnowledge DetermineResultingType(TypeKnowledge a, TypeKnowledge b)
        {
            var oa = a.GetTypeObject();
            var ob = b.GetTypeObject();

            var definingTypesInPriotizedOrder = new[] {typeof (string), typeof (float), typeof (double)};
            foreach (var type in definingTypesInPriotizedOrder)
            {
                if (oa == type || ob == type)
                {
                    return new TypeKnowledge(type);
                }
            }

            return a;
        }
    }
}