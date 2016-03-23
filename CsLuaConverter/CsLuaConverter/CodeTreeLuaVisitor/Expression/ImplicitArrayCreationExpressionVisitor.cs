namespace CsLuaConverter.CodeTreeLuaVisitor.Expression
{
    using System.CodeDom.Compiler;
    using CodeTree;
    using Providers;

    public class ImplicitArrayCreationExpressionVisitor : BaseVisitor
    {
        public ImplicitArrayCreationExpressionVisitor(CodeTreeBranch branch) : base(branch)
        {
        }

        public override void Visit(IndentedTextWriter textWriter, IProviders providers)
        {
            throw new System.NotImplementedException();
        }
    }
}