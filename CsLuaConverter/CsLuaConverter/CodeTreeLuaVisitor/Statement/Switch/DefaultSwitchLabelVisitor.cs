namespace CsLuaConverter.CodeTreeLuaVisitor.Statement.Switch
{
    using CodeTree;
    using CsLuaConverter.Context;
    using CsLuaConverter.SyntaxExtensions;

    using Microsoft.CodeAnalysis.CSharp.Syntax;

    public class DefaultSwitchLabelVisitor : BaseVisitor, ISwitchLabelVisitor
    {
        public DefaultSwitchLabelVisitor(CodeTreeBranch branch) : base(branch)
        {
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IContext context)
        {
            var syntax = (DefaultSwitchLabelSyntax)this.Branch.SyntaxNode;
            syntax.Write(textWriter, context);
        }
        
    }
}