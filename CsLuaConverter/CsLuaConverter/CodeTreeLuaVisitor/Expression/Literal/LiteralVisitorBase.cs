namespace CsLuaConverter.CodeTreeLuaVisitor.Expression.Literal
{
    using System;
    using CodeTree;
    using Providers;
    using Providers.TypeKnowledgeRegistry;

    public class LiteralVisitorBase : BaseVisitor
    {
        private readonly string text;
        private readonly TypeKnowledge resultingTypeKnowledge;

        protected LiteralVisitorBase(CodeTreeBranch branch, Type resultingType, string resultingText = null) : base(branch)
        {
            this.text = resultingText ?? ((CodeTreeLeaf)this.Branch.Nodes[0]).Text;
            this.resultingTypeKnowledge = new TypeKnowledge(resultingType);
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            textWriter.Write(this.text);
            providers.TypeKnowledgeRegistry.CurrentType = this.resultingTypeKnowledge;
        }
    }
}