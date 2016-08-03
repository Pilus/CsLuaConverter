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
        private readonly TypeKnowledge resultingType;

        protected BinaryExpressionVisitorBase(CodeTreeBranch branch, SyntaxKind expectedTokenKind, string alternativeText = null, TypeKnowledge resultingType = null) : base(branch)
        {
            this.ExpectKind(1, expectedTokenKind);
            this.token = alternativeText ?? ((CodeTreeLeaf) this.Branch.Nodes[1]).Text;
            this.lhsVisitor = this.CreateVisitor(0);
            this.rhsVisitor = this.CreateVisitor(2);
            this.resultingType = resultingType;
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            providers.Context.CurrentType = null;
            this.lhsVisitor.Visit(textWriter, providers);
            var lhsType = providers.Context.CurrentType;

            textWriter.Write($" {this.token} ");
            providers.Context.CurrentType = null;
            this.rhsVisitor.Visit(textWriter, providers);
            var rhsType = providers.Context.CurrentType;

            providers.Context.CurrentType = this.resultingType ?? DetermineResultingType(lhsType, rhsType);
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