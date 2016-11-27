namespace CsLuaConverter.CodeTreeLuaVisitor.Expression
{
    using CodeTree;
    using CsLuaConverter.Context;
    using CsLuaConverter.SyntaxExtensions;

    using Microsoft.CodeAnalysis.CSharp.Syntax;

    public class UnaryMinusExpressionVisitor : BaseVisitor
    {

        public UnaryMinusExpressionVisitor(CodeTreeBranch branch) : base(branch)
        {

        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IContext context)
        {
            var syntax = (PrefixUnaryExpressionSyntax)this.Branch.SyntaxNode;
            textWriter.Write("-");
            syntax.Operand.Write(textWriter, context);
        }
    }
}