namespace CsLuaConverter.CodeTreeLuaVisitor.Constraint
{
    using System;
    using CodeTree;
    using Name;
    using Providers;
    using Providers.TypeKnowledgeRegistry;
    using Type;

    public class TypeConstraintVisitor : BaseVisitor, IConstraint
    {
        private readonly ITypeVisitor typeVisitor;

        public TypeConstraintVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.typeVisitor = (ITypeVisitor) this.CreateVisitor(0);
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            throw new System.NotImplementedException();
        }

        public TypeKnowledge GetConstrainedType(IProviders providers)
        {
            return this.typeVisitor.GetType(providers);
        }
    }
}