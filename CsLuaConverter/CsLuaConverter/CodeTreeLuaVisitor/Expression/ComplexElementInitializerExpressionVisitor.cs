namespace CsLuaConverter.CodeTreeLuaVisitor.Expression
{
    using CodeTree;
    using CsLuaConverter.Context;
    using CsLuaConverter.SyntaxExtensions;

    using Microsoft.CodeAnalysis.CSharp.Syntax;

    public class ComplexElementInitializerExpressionVisitor : BaseVisitor
    {


        public ComplexElementInitializerExpressionVisitor(CodeTreeBranch branch) : base(branch)
        {

        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IContext context)
        {
            var syntax = (InitializerExpressionSyntax)this.Branch.SyntaxNode;
            syntax.Write(textWriter, context);
        }
    }
}