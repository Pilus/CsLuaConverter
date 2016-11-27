namespace CsLuaConverter.CodeTreeLuaVisitor.Constraint
{
    using CodeTree;
    using CsLuaConverter.Context;

    public class TypeConstraintVisitor : BaseVisitor, IConstraint
    {
        private readonly IVisitor typeVisitor;

        public TypeConstraintVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.typeVisitor = this.CreateVisitor(0);
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IContext context)
        {
            throw new System.NotImplementedException();
        }
    }
}