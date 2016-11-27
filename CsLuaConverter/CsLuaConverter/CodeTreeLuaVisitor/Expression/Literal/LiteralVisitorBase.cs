namespace CsLuaConverter.CodeTreeLuaVisitor.Expression.Literal
{
    using CodeTree;
    using CsLuaConverter.Context;
    using CsLuaConverter.SyntaxExtensions;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    public class LiteralVisitorBase : BaseVisitor
    {
        private readonly string text;

        protected LiteralVisitorBase(CodeTreeBranch branch, string resultingText = null) : base(branch)
        {
            this.text = resultingText ?? ((CodeTreeLeaf)this.Branch.Nodes[0]).Text;
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IContext context)
        {
            var syntax = (LiteralExpressionSyntax) this.Branch.SyntaxNode;
            syntax.Write(textWriter, context);
        }
    }
}