namespace CsLuaConverter.CodeTreeLuaVisitor.Expression
{
    using System.CodeDom.Compiler;
    using CodeTree;
    using Lists;
    using Microsoft.CodeAnalysis.CSharp;
    using Providers;
    using Type;

    public class ObjectCreationExpressionVisitor : BaseVisitor
    {
        private readonly ITypeVisitor objectTypeVisitor;
        private readonly ArgumentListVisitor constructorArgumentsVisitor;
        private readonly CollectionInitializerExpressionVisitor initializer;

        public ObjectCreationExpressionVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.ExpectKind(0, SyntaxKind.NewKeyword);
            this.objectTypeVisitor = (ITypeVisitor) this.CreateVisitor(1);
            this.constructorArgumentsVisitor = (ArgumentListVisitor)this.CreateVisitor(2);

            if (this.IsKind(3, SyntaxKind.CollectionInitializerExpression))
            {
                this.initializer = (CollectionInitializerExpressionVisitor)this.CreateVisitor(3);
            }
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            this.objectTypeVisitor.WriteAsReference(textWriter, providers);
            var type = this.objectTypeVisitor.GetType(providers);
            providers.TypeKnowledgeRegistry.CurrentType = type.GetConstructor();
            this.constructorArgumentsVisitor.Visit(textWriter, providers);

            this.initializer?.Visit(textWriter, providers);

            providers.TypeKnowledgeRegistry.CurrentType = type;
        }
    }
}