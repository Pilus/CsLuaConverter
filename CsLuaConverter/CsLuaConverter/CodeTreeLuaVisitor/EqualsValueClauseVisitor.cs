namespace CsLuaConverter.CodeTreeLuaVisitor
{
    using CodeTree;
    using CsLuaConverter.Context;
    using CsLuaConverter.SyntaxExtensions;

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
            Visit(this.Syntax, textWriter, context);
        }

        public static void Visit(EqualsValueClauseSyntax syntax, IIndentedTextWriterWrapper textWriter, IContext context)
        {
            textWriter.Write(" = ");
            syntax.Value.Write(textWriter, context);
        }
    }
}