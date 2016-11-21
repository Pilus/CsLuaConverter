namespace CsLuaConverter.CodeTreeLuaVisitor
{
    using CodeTree;
    using CsLuaConverter.Context;
    using Microsoft.CodeAnalysis.CSharp;
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