namespace CsLuaConverter.CodeTreeLuaVisitor.Expression
{
    using CodeTree;
    using CsLuaConverter.Context;
    using CsLuaConverter.SyntaxExtensions;

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
            symbol.Write(textWriter, context);
        }
    }
}