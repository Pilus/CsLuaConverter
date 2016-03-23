namespace CsLuaConverter.CodeTreeLuaVisitor.Expression
{
    using System.CodeDom.Compiler;
    using CodeTree;
    using Providers;

    public class StringLiteralExpressionVisitor : BaseVisitor
    {
        public StringLiteralExpressionVisitor(CodeTreeBranch branch) : base(branch)
        {
        }

        public override void Visit(IndentedTextWriter textWriter, IProviders providers)
        {
            throw new System.NotImplementedException();
        }
    }
}