namespace CsLuaConverter.CodeTreeLuaVisitor
{
    using CodeTree;
    using CsLuaSyntaxTranslator;
    using CsLuaSyntaxTranslator.Context;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    public class UsingDirectiveVisitor : SyntaxVisitorBase<UsingDirectiveSyntax>
    {
        public UsingDirectiveVisitor(CodeTreeBranch branch) : base(branch)
        {
        }

        public UsingDirectiveVisitor(UsingDirectiveSyntax syntax) : base(syntax)
        {
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IContext context)
        {
        }
    }
}