namespace CsLuaConverter.CodeTreeLuaVisitor.Expression
{
    using CsLuaConverter.CodeTree;
    using CsLuaConverter.Providers;

    public class ConditionalAccessExpressionVisitor : BaseVisitor
    {
        public ConditionalAccessExpressionVisitor(CodeTreeBranch branch)
            : base(branch)
        {
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            throw new System.NotImplementedException();
        }
    }
}