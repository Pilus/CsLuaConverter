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

        public ObjectCreationExpressionVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.ExpectKind(0, SyntaxKind.NewKeyword);
            this.objectTypeVisitor = (ITypeVisitor) this.CreateVisitor(1);
            this.constructorArgumentsVisitor = (ArgumentListVisitor)this.CreateVisitor(2);
        }

        public override void Visit(IndentedTextWriter textWriter, IProviders providers)
        {
            this.objectTypeVisitor.WriteAsReference(textWriter, providers);
            this.constructorArgumentsVisitor.Visit(textWriter, providers);
            providers.TypeKnowledgeRegistry.CurrentType = this.objectTypeVisitor.GetType(providers);
        }
    }
}