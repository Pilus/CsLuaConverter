namespace CsLuaConverter.CodeTreeLuaVisitor
{
    using CodeTree;
    using CsLuaSyntaxTranslator;
    using CsLuaSyntaxTranslator.Context;
    using CsLuaSyntaxTranslator.SyntaxExtensions;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    public class ParameterVisitor : SyntaxVisitorBase<ParameterSyntax>
    {
        public ParameterVisitor(CodeTreeBranch branch) : base(branch)
        {
        }

        public ParameterVisitor(ParameterSyntax syntax) : base(syntax)
        {
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IContext context)
        {
            this.Syntax.Write(textWriter, context);
        }
    }
}