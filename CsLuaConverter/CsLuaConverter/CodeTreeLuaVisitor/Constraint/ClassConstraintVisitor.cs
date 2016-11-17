namespace CsLuaConverter.CodeTreeLuaVisitor.Constraint
{
    using CodeTree;
    using CsLuaConverter.Context;

    public class ClassConstraintVisitor : BaseVisitor, IConstraint
    {
        public ClassConstraintVisitor(CodeTreeBranch branch) : base(branch)
        {
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IContext context)
        {
        }
    }
}