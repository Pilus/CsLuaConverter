namespace CsLuaConverter.CodeTreeLuaVisitor.Statement
{
    using System.CodeDom.Compiler;
    using CodeTree;
    using Expression;
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

        public override void Visit(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            if (this.targetVisitor is ThisExpressionVisitor)
            {
                providers.TypeKnowledgeRegistry.CurrentType = providers.NameProvider.GetScopeElement("this").Type;
                textWriter.Write("(element % _M.DOT_LVL(typeObject.Level)).");
            }
            else
            {
                providers.TypeKnowledgeRegistry.CurrentType = null;
                textWriter.Write("(");
                this.targetVisitor.Visit(textWriter, providers);
                textWriter.Write("% _M.DOT).");
            }

            this.indexVisitor.Visit(textWriter, providers);
        }
    }
}