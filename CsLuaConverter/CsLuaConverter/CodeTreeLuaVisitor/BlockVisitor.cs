namespace CsLuaConverter.CodeTreeLuaVisitor
{
    using CodeTree;
    using CsLuaSyntaxTranslator;
    using CsLuaSyntaxTranslator.Context;
    using CsLuaSyntaxTranslator.SyntaxExtensions;
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
            this.Syntax.Write(textWriter, context);
        }
    }
}