namespace CsLuaConverter.CodeTreeLuaVisitor
{
    using System.CodeDom.Compiler;
    using CodeTree;
    using Microsoft.CodeAnalysis.CSharp;
    using Providers;
    using Providers.TypeProvider;
    using Type;

    public class ParameterVisitor : BaseVisitor
    {
        private readonly bool isParams;
        private readonly ITypeVisitor type;
        private readonly string name;

        public ParameterVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.isParams = this.Branch.Nodes[0].Kind.Equals(SyntaxKind.ParamsKeyword);
            var i = this.isParams ? 1 : 0;

            if (!this.Branch.Nodes[i].Kind.Equals(SyntaxKind.IdentifierToken))
            {
                this.type = (ITypeVisitor)this.CreateVisitor(i);
                i++;
            }

            this.ExpectKind(i, SyntaxKind.IdentifierToken);
            this.name = ((CodeTreeLeaf) this.Branch.Nodes[i]).Text;
        }

        public override void Visit(IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.Write(this.name);
            providers.NameProvider.AddToScope(new ScopeElement(this.name, this.type?.GetType(providers) ?? providers.TypeKnowledgeRegistry.CurrentType));
        }

        public void WriteAsTypes(IndentedTextWriter textWriter, IProviders providers)
        {
            this.type.WriteAsType(textWriter, providers);
        }
    }
}