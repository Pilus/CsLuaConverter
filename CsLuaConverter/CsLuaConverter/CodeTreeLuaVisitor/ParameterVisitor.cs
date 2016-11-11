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
        private readonly IVisitor type;
        private readonly string name;

        public ParameterVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.isParams = this.Branch.Nodes[0].Kind.Equals(SyntaxKind.ParamsKeyword);
            var i = this.isParams ? 1 : 0;

            if (!this.Branch.Nodes[i].Kind.Equals(SyntaxKind.IdentifierToken))
            {
                this.type = this.CreateVisitor(i);
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
        }
    }
}