namespace CsLuaConverter.CodeTreeLuaVisitor
{
    using CodeTree;
    using CsLuaConverter.Context;
    using Microsoft.CodeAnalysis.CSharp;
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
            textWriter.Write(" = ");
            VisitNode(this.Syntax.Value, textWriter, context);
        }
    }
}