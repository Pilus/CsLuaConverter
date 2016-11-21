namespace CsLuaConverter.CodeTreeLuaVisitor.Member
{
    using System.Linq;

    using CodeTree;
    using CsLuaConverter.Context;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    using Type;

    public class VariableDeclarationVisitor : BaseVisitor
    {
        private readonly VariableDeclaratorVisitor declaratorVisitor;
        public VariableDeclarationVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.ExpectKind(1, SyntaxKind.VariableDeclarator);
            this.declaratorVisitor = (VariableDeclaratorVisitor)this.CreateVisitor(1);
        }

        public string GetName()
        {
            return this.declaratorVisitor.GetName();
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IContext context)
        {
            textWriter.Write("local ");
            this.declaratorVisitor.Visit(textWriter, context);
        }

        public static void WriteDefaultValue(VariableDeclarationSyntax syntax, IIndentedTextWriterWrapper textWriter, IContext context)
        {
            VariableDeclaratorVisitor.WriteDefaultValue(syntax.Variables.Single(), textWriter, context);
        }

        public void WriteInitializeValue(IIndentedTextWriterWrapper textWriter, IContext context)
        {
            this.declaratorVisitor.WriteInitializeValue(textWriter, context);
        }
    }
}