namespace CsLuaConverter.CodeTreeLuaVisitor.Expression
{
    using CodeTree;
    using Microsoft.CodeAnalysis.CSharp;
    using Providers;
    using Type;

    public class AsExpressionVisitor : BaseVisitor
    {
        private readonly IVisitor target;
        private readonly ITypeVisitor type;

        public AsExpressionVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.target = this.CreateVisitor(0);
            this.ExpectKind(1, SyntaxKind.AsKeyword);
            this.type = (ITypeVisitor) this.CreateVisitor(2);
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            this.type.WriteAsReference(textWriter, providers);
            textWriter.Write(".__as(");
            providers.TypeKnowledgeRegistry.CurrentType = null;
            this.target.Visit(textWriter, providers);
            textWriter.Write(")");
            providers.TypeKnowledgeRegistry.CurrentType = this.type.GetType(providers);
        }
    }
}