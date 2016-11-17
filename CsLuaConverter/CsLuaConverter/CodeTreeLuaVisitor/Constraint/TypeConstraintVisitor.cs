namespace CsLuaConverter.CodeTreeLuaVisitor.Constraint
{
    using System;
    using CodeTree;
    using Name;
    using Providers;
    using Type;

    public class TypeConstraintVisitor : BaseVisitor, IConstraint
    {
        private readonly IVisitor typeVisitor;

        public TypeConstraintVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.typeVisitor = this.CreateVisitor(0);
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            throw new System.NotImplementedException();
        }
    }
}