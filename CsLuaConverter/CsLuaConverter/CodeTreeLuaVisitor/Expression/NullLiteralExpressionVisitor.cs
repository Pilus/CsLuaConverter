namespace CsLuaConverter.CodeTreeLuaVisitor.Expression
{
    using System.CodeDom.Compiler;
    using CodeTree;
    using Providers;

    public class NullLiteralExpressionVisitor : BaseVisitor
    {
        public NullLiteralExpressionVisitor(CodeTreeBranch branch) : base(branch)
        {
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            throw new System.NotImplementedException();
        }
    }
}