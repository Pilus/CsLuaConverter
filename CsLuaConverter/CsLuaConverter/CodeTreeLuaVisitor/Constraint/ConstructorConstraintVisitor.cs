namespace CsLuaConverter.CodeTreeLuaVisitor.Constraint
{
    using CodeTree;
    using CsLuaConverter.Context;

    public class ConstructorConstraintVisitor : BaseVisitor, IConstraint
    {
        public ConstructorConstraintVisitor(CodeTreeBranch branch) : base(branch)
        {
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IContext context)
        {
            throw new System.NotImplementedException();
        }
    }
}