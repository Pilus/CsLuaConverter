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

            var i = 2;
            if (this.IsKind(i, SyntaxKind.ArgumentList))
            {
                this.constructorArgumentsVisitor = (ArgumentListVisitor)this.CreateVisitor(i);
                i++;
            }            

            if (this.IsKind(i, SyntaxKind.CollectionInitializerExpression) || this.IsKind(i, SyntaxKind.ObjectInitializerExpression))
            {
                this.initializer = this.CreateVisitor(i);
            }
            else if (this.Branch.Nodes.Length > i)
            {
                throw new VisitorException($"Unknown following argument to object creation: {branch.Nodes[3].Kind}.");
            }
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            textWriter.Write(this.initializer != null ? "(" : "");

            this.objectTypeVisitor.WriteAsReference(textWriter, providers);
            var type = this.objectTypeVisitor.GetType(providers);
            textWriter.Write("._C_0_");

            if (this.constructorArgumentsVisitor != null)
            { 
                providers.Context.PossibleMethods = new PossibleMethods(type.GetConstructors());

                var cstorArgsWriter = textWriter.CreateTextWriterAtSameIndent();
                this.constructorArgumentsVisitor.Visit(cstorArgsWriter, providers);

                var method = providers.Context.PossibleMethods.GetOnlyRemainingMethodOrThrow();
            
                method.WriteSignature(textWriter, providers);

                textWriter.AppendTextWriter(cstorArgsWriter);
            }
            else
            {
                textWriter.Write("0()");    
            }

            providers.Context.PossibleMethods = null;
            providers.Context.CurrentType = null;

            if (this.initializer != null)
            {
                textWriter.Write(" % _M.DOT)");
                providers.Context.CurrentType = type;
                this.initializer.Visit(textWriter, providers);
            }
            

            providers.Context.CurrentType = type;
        }
    }
}