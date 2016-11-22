namespace CsLuaConverter.CodeTreeLuaVisitor
{
    using System.Linq;

    using CodeTree;
    using CsLuaConverter.Context;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;
    using Type;

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
            Visit(this.Syntax, textWriter, context);
        }

        public static void Visit(ParameterSyntax syntax, IIndentedTextWriterWrapper textWriter, IContext context)
        {
            if (syntax.Modifiers.Any(mod => mod.Kind() == SyntaxKind.ParamsKeyword))
            {
                textWriter.Write("firstParam, ...");
            }
            else
            {
                textWriter.Write(syntax.Identifier.Text);
            }
        }
    }
}