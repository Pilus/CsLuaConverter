namespace CsLuaConverter.CodeTreeLuaVisitor.Statement
{
    using CodeTree;
    using CsLuaConverter.Context;
    using CsLuaConverter.SyntaxExtensions;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    public class ForEachStatementVisitor : BaseVisitor
    {
        public ForEachStatementVisitor(CodeTreeBranch branch) : base(branch)
        {
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IContext context)
        {
            var syntax = (ForEachStatementSyntax)this.Branch.SyntaxNode;

            textWriter.Write("for _,{0} in (", syntax.Identifier.Text);
            syntax.Expression.Write(textWriter, context);
            textWriter.WriteLine(" % _M.DOT).GetEnumerator() do");

            syntax.Statement.Write(textWriter, context);
            textWriter.WriteLine("end");
        }
    }
}