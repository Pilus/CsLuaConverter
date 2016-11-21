namespace CsLuaConverter.CodeTreeLuaVisitor
{
    using System.Linq;
    using CodeTree;
    using CsLuaConverter.Context;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    public class ArgumentVisitor : SyntaxVisitorBase<ArgumentSyntax>
    {
        public ArgumentVisitor(CodeTreeBranch branch) : base(branch)
        {
        }

        public ArgumentVisitor(ArgumentSyntax syntax) : base(syntax)
        {
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IContext context)
        {
            VisitNode(this.Syntax.Expression, textWriter, context);
        }
    }
}