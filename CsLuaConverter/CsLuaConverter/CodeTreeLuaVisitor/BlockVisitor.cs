namespace CsLuaConverter.CodeTreeLuaVisitor
{
    using CodeTree;
    using CsLuaConverter.Context;
    using Filters;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    public class BlockVisitor : SyntaxVisitorBase<BlockSyntax>
    {
        public BlockVisitor(CodeTreeBranch branch) : base(branch)
        {
        }
        public BlockVisitor(BlockSyntax syntax) : base(syntax)
        {
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IContext context)
        {
            Visit(this.Syntax, textWriter, context);
        }

        public static void Visit(BlockSyntax syntax, IIndentedTextWriterWrapper textWriter, IContext context)
        {
            textWriter.Indent++;
            VisitAllNodes(syntax.Statements, textWriter, context);
            textWriter.Indent--;
        }
    }
}