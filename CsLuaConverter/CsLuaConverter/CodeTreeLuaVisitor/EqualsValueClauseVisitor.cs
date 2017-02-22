namespace CsLuaConverter.CodeTreeLuaVisitor
{
    using CodeTree;
    using CsLuaSyntaxTranslator;
    using CsLuaSyntaxTranslator.Context;
    using CsLuaSyntaxTranslator.SyntaxExtensions;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    public class EqualsValueClauseVisitor : SyntaxVisitorBase<EqualsValueClauseSyntax>
    {
        public EqualsValueClauseVisitor(CodeTreeBranch branch) : base(branch)
        {
        }

        public EqualsValueClauseVisitor(EqualsValueClauseSyntax syntax) : base(syntax)
        {
            
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IContext context)
        {
            this.Syntax.Write(textWriter, context);
        }
    }
}