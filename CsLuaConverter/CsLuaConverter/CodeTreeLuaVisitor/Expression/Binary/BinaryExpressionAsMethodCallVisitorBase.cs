namespace CsLuaConverter.CodeTreeLuaVisitor.Expression.Binary
{
    using CodeTree;
    using Microsoft.CodeAnalysis.CSharp;
    using Providers;

    public class BinaryExpressionAsMethodCallVisitorBase : BaseVisitor
    {
        private readonly IVisitor lhsVisitor;
        private readonly IVisitor rhsVisitor;
        private readonly string methodName;

        public BinaryExpressionAsMethodCallVisitorBase(CodeTreeBranch branch, SyntaxKind expectedKind, string methodName) : base(branch)
        {
            this.ExpectKind(1, expectedKind);
            this.lhsVisitor = this.CreateVisitor(0);
            this.rhsVisitor = this.CreateVisitor(2);
            this.methodName = methodName;
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            textWriter.Write($"{this.methodName}(");
            providers.TypeKnowledgeRegistry.CurrentType = null;
            this.lhsVisitor.Visit(textWriter, providers);
            var lhsType = providers.TypeKnowledgeRegistry.CurrentType;

            textWriter.Write(", ");
            providers.TypeKnowledgeRegistry.CurrentType = null;
            this.rhsVisitor.Visit(textWriter, providers);

            textWriter.Write(")");

            providers.TypeKnowledgeRegistry.CurrentType = lhsType;
        }
    }
}