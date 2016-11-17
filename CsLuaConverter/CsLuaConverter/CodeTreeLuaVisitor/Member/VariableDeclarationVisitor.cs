namespace CsLuaConverter.CodeTreeLuaVisitor.Member
{
    using CodeTree;
    using CsLuaConverter.Context;
    using Microsoft.CodeAnalysis.CSharp;
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

        public void WriteDefaultValue(IIndentedTextWriterWrapper textWriter, IContext context)
        {
            this.declaratorVisitor.WriteDefaultValue(textWriter, context);
        }

        public void WriteInitializeValue(IIndentedTextWriterWrapper textWriter, IContext context)
        {
            this.declaratorVisitor.WriteInitializeValue(textWriter, context);
        }
    }
}