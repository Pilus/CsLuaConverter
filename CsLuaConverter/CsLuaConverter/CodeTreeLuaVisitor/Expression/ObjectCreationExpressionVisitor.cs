namespace CsLuaConverter.CodeTreeLuaVisitor.Expression
{
    using CodeTree;
    using CsLuaConverter.Context;
    using CsLuaConverter.SyntaxExtensions;

    using Microsoft.CodeAnalysis.CSharp.Syntax;

    public class ObjectCreationExpressionVisitor : BaseVisitor
    {
        public ObjectCreationExpressionVisitor(CodeTreeBranch branch) : base(branch)
        {
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IContext context)
        {
            var syntax = (ObjectCreationExpressionSyntax)this.Branch.SyntaxNode;
            syntax.Write(textWriter, context);
        }
    }
}