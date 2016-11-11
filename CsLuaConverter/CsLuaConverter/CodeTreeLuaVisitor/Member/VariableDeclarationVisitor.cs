namespace CsLuaConverter.CodeTreeLuaVisitor.Member
{
    using CodeTree;
    using Microsoft.CodeAnalysis.CSharp;
    using Providers;
    using Providers.TypeProvider;
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

        public override void Visit(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            textWriter.Write("local ");
            this.declaratorVisitor.Visit(textWriter, providers);
        }

        public void WriteDefaultValue(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            this.declaratorVisitor.WriteDefaultValue(textWriter, providers);
        }

        public void WriteInitializeValue(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            this.declaratorVisitor.WriteInitializeValue(textWriter, providers);
        }
    }
}