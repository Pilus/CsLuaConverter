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
            var paramsOffset = this.isParams ? 1 : 0;
            this.ExpectKind(1 + paramsOffset, SyntaxKind.IdentifierToken);
            this.type = (ITypeVisitor) this.CreateVisitor(0 + paramsOffset);
            this.name = ((CodeTreeLeaf) this.Branch.Nodes[1 + paramsOffset]).Text;
        }

        public override void Visit(IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.Write(this.name);
            providers.NameProvider.AddToScope(new ScopeElement(this.name, this.type.GetType(providers)));
        }

        public void WriteAsTypes(IndentedTextWriter textWriter, IProviders providers)
        {
            this.type.WriteAsType(textWriter, providers);
        }
    }
}