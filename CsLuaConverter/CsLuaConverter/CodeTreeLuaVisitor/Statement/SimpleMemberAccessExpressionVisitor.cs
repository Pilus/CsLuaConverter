namespace CsLuaConverter.CodeTreeLuaVisitor.Statement
{
    using System.CodeDom.Compiler;
    using CodeTree;
    using Microsoft.CodeAnalysis.CSharp;
    using Name;
    using Providers;

    public class SimpleMemberAccessExpressionVisitor : BaseVisitor
    {
        private readonly IVisitor targetVisitor;
        private readonly INameVisitor indexVisitor;
        public SimpleMemberAccessExpressionVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.ExpectKind(1, SyntaxKind.DotToken);
            this.targetVisitor = this.CreateVisitor(0);
            this.indexVisitor = (INameVisitor)this.CreateVisitor(2);
        }

        public override void Visit(IndentedTextWriter textWriter, IProviders providers)
        {
            providers.TypeKnowledgeRegistry.CurrentType = null;
            this.targetVisitor.Visit(textWriter, providers);

            textWriter.Write(".");

            var targetType = providers.TypeKnowledgeRegistry.CurrentType;
            providers.TypeKnowledgeRegistry.CurrentType = targetType.GetTypeKnowledgeForSubElement(this.indexVisitor.GetName());
        }
    }
}