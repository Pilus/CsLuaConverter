namespace CsLuaConverter.CodeTreeLuaVisitor
{
    using System.Linq;
    using CodeTree;
    using CsLuaConverter.Context;
    using CsLuaConverter.SyntaxExtensions;
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
            this.Syntax.Write(textWriter, context);
        }
    }
}