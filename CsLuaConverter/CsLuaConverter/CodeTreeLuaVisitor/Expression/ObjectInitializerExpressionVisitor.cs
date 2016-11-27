namespace CsLuaConverter.CodeTreeLuaVisitor.Expression
{
    using CodeTree;
    using CsLuaConverter.Context;
    using CsLuaConverter.SyntaxExtensions;

    using Filters;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    public class ObjectInitializerExpressionVisitor : BaseVisitor
    {
        private readonly BaseVisitor[] innerVisitors;

        public ObjectInitializerExpressionVisitor(CodeTreeBranch branch) : base(branch)
        {

        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IContext context)
        {
            var symbol = (InitializerExpressionSyntax)this.Branch.SyntaxNode;
            // Note, there are 4 different implementations using InitializerExpressionSyntax
            textWriter.Write(".__Initialize({");

            if (symbol.Expressions.Count > 1)
                textWriter.WriteLine();

            textWriter.Indent++;
            symbol.Expressions.Write(ExpressionExtensions.Write, textWriter, context);
            textWriter.Indent--;
            
            if (symbol.Expressions.Count > 1)
                textWriter.WriteLine();

            textWriter.Write("})");
        }
    }
}