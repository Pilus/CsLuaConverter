namespace CsLuaConverter.CodeTreeLuaVisitor.Constraint
{
    using CodeTree;
    using Providers;

    public class ConstructorConstraintVisitor : BaseVisitor, IConstraint
    {
        public ConstructorConstraintVisitor(CodeTreeBranch branch) : base(branch)
        {
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            throw new System.NotImplementedException();
        }
    }
}