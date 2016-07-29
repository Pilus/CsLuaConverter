namespace CsLuaConverter.CodeTreeLuaVisitor.Expression
{
    using CodeTree;
    using Microsoft.CodeAnalysis.CSharp;
    using Providers;

    public class ConditionalExpressionVisitor : BaseVisitor
    {
        private readonly IVisitor condition;
        private readonly IVisitor trueStatement;
        private readonly IVisitor falseStatement;

        public ConditionalExpressionVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.ExpectKind(1, SyntaxKind.QuestionToken);
            this.ExpectKind(3, SyntaxKind.ColonToken);
            this.condition = this.CreateVisitor(0);
            this.trueStatement = this.CreateVisitor(2);
            this.falseStatement = this.CreateVisitor(4);
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            this.condition.Visit(textWriter, providers);

            textWriter.Write(" and ");
            providers.TypeKnowledgeRegistry.CurrentType = null;
            this.trueStatement.Visit(textWriter, providers);
            var valueType1 = providers.TypeKnowledgeRegistry.CurrentType;

            textWriter.Write(" or ");
            providers.TypeKnowledgeRegistry.CurrentType = null;
            this.falseStatement.Visit(textWriter, providers);

            providers.TypeKnowledgeRegistry.CurrentType = providers.TypeKnowledgeRegistry.CurrentType ?? valueType1;
        }
    }
}