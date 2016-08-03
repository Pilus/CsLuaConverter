namespace CsLuaConverter.CodeTreeLuaVisitor.Member
{
    using CodeTree;
    using Microsoft.CodeAnalysis.CSharp;
    using Providers;
    using Providers.TypeProvider;
    using Type;

    public class VariableDeclarationVisitor : BaseVisitor
    {
        private readonly ITypeVisitor typeVisitor;
        private readonly VariableDeclaratorVisitor declaratorVisitor;
        public VariableDeclarationVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.typeVisitor = (ITypeVisitor)this.CreateVisitor(0);
            this.ExpectKind(1, SyntaxKind.VariableDeclarator);
            this.declaratorVisitor = (VariableDeclaratorVisitor)this.CreateVisitor(1);
        }

        public string GetName()
        {
            return this.declaratorVisitor.GetName();
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            var type = this.typeVisitor.GetType(providers);

            textWriter.Write("local ");
            this.declaratorVisitor.Visit(textWriter, providers);

            providers.NameProvider.AddToScope(new ScopeElement(this.declaratorVisitor.GetName(), type ?? providers.Context.CurrentType));
            providers.Context.CurrentType = null;
        }

        public void WriteDefaultValue(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            this.declaratorVisitor.WriteDefaultValue(textWriter, providers, this.typeVisitor);
        }

        public void WriteInitializeValue(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            this.declaratorVisitor.WriteInitializeValue(textWriter, providers);
        }
    }
}