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
            this.lhsVisitor.Visit(textWriter, providers);

            textWriter.Write($" {this.token} ");
            this.rhsVisitor.Visit(textWriter, providers);
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