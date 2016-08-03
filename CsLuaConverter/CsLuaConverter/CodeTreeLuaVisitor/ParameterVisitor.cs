namespace CsLuaConverter.CodeTreeLuaVisitor
{
    using CodeTree;
    using Microsoft.CodeAnalysis.CSharp;
    using Providers;
    using Providers.TypeKnowledgeRegistry;
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

        public override void Visit(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            if (this.isParams)
            {
                textWriter.Write("firstParam, ...");
            }
            else
            {
                textWriter.Write(this.name);
            }

            providers.NameProvider.AddToScope(new ScopeElement(this.name, this.type?.GetType(providers) ?? providers.Context.CurrentType ?? providers.Context.ExpectedType));
            providers.Context.CurrentType = null;
        }

        public void WriteAsTypes(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            if (this.isParams)
            {
                this.type.GetType(providers).GetArrayGeneric().WriteAsType(textWriter, providers);
            }
            else
            {
                this.type.WriteAsType(textWriter, providers);
            }
        }

        public TypeKnowledge GetType(IProviders providers)
        {
            var tk = this.type?.GetType(providers);

            if (tk != null)
            {
                tk.IsParams = this.isParams;
            }
            
            return tk;
        }

        public string GetName()
        {
            return this.name;
        }
    }
}