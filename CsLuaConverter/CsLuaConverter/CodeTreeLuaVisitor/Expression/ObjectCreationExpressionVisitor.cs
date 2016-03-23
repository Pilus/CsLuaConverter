namespace CsLuaConverter.CodeTreeLuaVisitor.Expression
{
    using System.CodeDom.Compiler;
    using CodeTree;
    using Providers;

    public class ObjectCreationExpressionVisitor : BaseVisitor
    {
        public ObjectCreationExpressionVisitor(CodeTreeBranch branch) : base(branch)
        {
        }

        public override void Visit(IndentedTextWriter textWriter, IProviders providers)
        {
            throw new System.NotImplementedException();
        }
    }
}