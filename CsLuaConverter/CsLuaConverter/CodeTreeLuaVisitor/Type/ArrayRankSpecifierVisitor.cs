namespace CsLuaConverter.CodeTreeLuaVisitor.Type
{
    using System.Linq;

    using CsLuaConverter.CodeTree;
    using CsLuaConverter.Context;
    using CsLuaConverter.SyntaxExtensions;

    using Microsoft.CodeAnalysis.CSharp.Syntax;

    public class ArrayRankSpecifierVisitor : BaseVisitor
    {
        public ArrayRankSpecifierVisitor(CodeTreeBranch branch) : base(branch)
        {
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IContext context)
        {
            var syntax = (ArrayRankSpecifierSyntax)this.Branch.SyntaxNode;

            if (syntax.Sizes.Count == 1 && syntax.Sizes.Single() is OmittedArraySizeExpressionSyntax)
            {
                textWriter.Write("._C_0_0()");
            }
            else
            {
                textWriter.Write("._C_0_2112(");
                syntax.Sizes.Write(ExpressionExtensions.Write, textWriter, context);
                textWriter.Write(")");
            }
        }
    }
}