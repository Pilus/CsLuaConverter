namespace CsLuaConverter.CodeTreeLuaVisitor.Expression
{
    using CodeTree;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Providers;

    public class ThisExpressionVisitor : BaseVisitor
    {
        public ThisExpressionVisitor(CodeTreeBranch branch) : base(branch)
        {
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IContext context)
        {
            textWriter.Write("element");
        }
    }
}