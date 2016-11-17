namespace CsLuaConverter.CodeTreeLuaVisitor.Expression.Literal
{
    using System;
    using System.Linq;
    using CodeTree;
    using Microsoft.CodeAnalysis.CSharp;
    using Providers;

    public class NumericLiteralExpressionVisitor : BaseVisitor
    {
        private readonly string value;

        public NumericLiteralExpressionVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.ExpectKind(0, SyntaxKind.NumericLiteralToken);
            this.value = ((CodeTreeLeaf) this.Branch.Nodes.Single()).Text;
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            textWriter.Write(this.value);
        }
    }
}