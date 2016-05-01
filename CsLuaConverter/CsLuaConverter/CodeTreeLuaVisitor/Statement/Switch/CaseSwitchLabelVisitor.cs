namespace CsLuaConverter.CodeTreeLuaVisitor.Statement.Switch
{
    using CodeTree;
    using Microsoft.CodeAnalysis.CSharp;
    using Providers;
    using Providers.TypeKnowledgeRegistry;

    public class CaseSwitchLabelVisitor : BaseVisitor, ISwitchLabelVisitor
    {
        private readonly IVisitor innerVisitor;

        public CaseSwitchLabelVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.ExpectKind(0, SyntaxKind.CaseKeyword);
            this.ExpectKind(2, SyntaxKind.ColonToken);
            this.innerVisitor = this.CreateVisitor(1);
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            textWriter.Write("switchValue == ");
            this.innerVisitor.Visit(textWriter, providers);
            providers.TypeKnowledgeRegistry.CurrentType = new TypeKnowledge(typeof (bool));
        }
    }
}