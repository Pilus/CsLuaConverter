namespace CsLuaConverter.CodeTreeLuaVisitor.Expression
{
    using CodeTree;
    using Lists;
    using Microsoft.CodeAnalysis.CSharp;
    using Providers;
    using Providers.TypeKnowledgeRegistry;
    using Type;

    public class ObjectCreationExpressionVisitor : BaseVisitor
    {
        private readonly ITypeVisitor objectTypeVisitor;
        private readonly ArgumentListVisitor constructorArgumentsVisitor;
        private readonly IVisitor initializer;

        public ObjectCreationExpressionVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.ExpectKind(0, SyntaxKind.NewKeyword);
            this.objectTypeVisitor = (ITypeVisitor) this.CreateVisitor(1);
            this.constructorArgumentsVisitor = (ArgumentListVisitor)this.CreateVisitor(2);

            if (this.IsKind(3, SyntaxKind.CollectionInitializerExpression) || this.IsKind(3, SyntaxKind.ObjectInitializerExpression))
            {
                this.initializer = this.CreateVisitor(3);
            }
            else if (this.Branch.Nodes.Length > 3)
            {
                throw new VisitorException($"Unknown following argument to object creation: {branch.Nodes[3].Kind}.");
            }
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            textWriter.Write(this.initializer != null ? "((" : "(");

            this.objectTypeVisitor.WriteAsReference(textWriter, providers);
            var type = this.objectTypeVisitor.GetType(providers);
            textWriter.Write(" % _M.DOT)._C_");
            providers.TypeKnowledgeRegistry.PossibleMethods = new PossibleMethods(type.GetConstructors());

            var cstorArgsWriter = textWriter.CreateTextWriterAtSameIndent();
            this.constructorArgumentsVisitor.Visit(cstorArgsWriter, providers);

            var method = providers.TypeKnowledgeRegistry.PossibleMethods.GetOnlyRemainingMethodOrThrow();
            
            method.WriteSignature(textWriter, providers);

            textWriter.AppendTextWriter(cstorArgsWriter);

            providers.TypeKnowledgeRegistry.PossibleMethods = null;
            providers.TypeKnowledgeRegistry.CurrentType = null;

            if (this.initializer != null)
            {
                textWriter.Write(" % _M.DOT)");
                providers.TypeKnowledgeRegistry.CurrentType = type;
                this.initializer.Visit(textWriter, providers);
            }
            

            providers.TypeKnowledgeRegistry.CurrentType = type;
        }
    }
}