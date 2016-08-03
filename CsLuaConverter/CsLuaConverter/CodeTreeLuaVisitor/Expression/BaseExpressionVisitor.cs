namespace CsLuaConverter.CodeTreeLuaVisitor.Expression
{
    using CodeTree;
    using Providers;

    public class BaseExpressionVisitor : BaseVisitor
    {
        public BaseExpressionVisitor(CodeTreeBranch branch) : base(branch)
        {
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            textWriter.Write("element");
            providers.Context.CurrentType = providers.NameProvider.GetScopeElement("base").Type;
        }
    }
}