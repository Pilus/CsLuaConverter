namespace CsLuaConverter.CodeTreeLuaVisitor.Statement.Switch
{
    using CodeTree;
    using CsLuaConverter.Context;
    using CsLuaConverter.SyntaxExtensions;

    using Microsoft.CodeAnalysis.CSharp.Syntax;

    public class CaseSwitchLabelVisitor : BaseVisitor, ISwitchLabelVisitor
    {
        public CaseSwitchLabelVisitor(CodeTreeBranch branch) : base(branch)
        {
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IContext context)
        {
            var syntax = (CaseSwitchLabelSyntax)this.Branch.SyntaxNode;
            textWriter.Write("switchValue == ");
            syntax.Value.Write(textWriter, context);
        }
    }
}