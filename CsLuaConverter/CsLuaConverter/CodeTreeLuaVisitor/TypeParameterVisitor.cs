namespace CsLuaConverter.CodeTreeLuaVisitor
{
    using CodeTree;
    using CsLuaConverter.Context;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    public class TypeParameterVisitor : SyntaxVisitorBase<TypeParameterSyntax>
    {
        public TypeParameterVisitor(CodeTreeBranch branch) : base(branch)
        {

        }

        public TypeParameterVisitor(TypeParameterSyntax syntax) : base(syntax)
        {
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IContext context)
        {
            Visit(this.Syntax, textWriter, context);
        }

        public static void Visit(TypeParameterSyntax syntax, IIndentedTextWriterWrapper textWriter, IContext context)
        {
            textWriter.Write(syntax.Identifier.Text);
        }
    }
}