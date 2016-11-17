namespace CsLuaConverter.CodeTreeLuaVisitor
{
    using System.Linq;

    using CodeTree;
    using Microsoft.CodeAnalysis.CSharp;
    using Providers;
    using Type;

    public class ParameterVisitor : BaseVisitor
    {
        private readonly bool isParams;
        private readonly IVisitor type;
        private readonly string name;

        public ParameterVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.isParams = this.Branch.Nodes[0].Kind.Equals(SyntaxKind.ParamsKeyword);
            this.name = branch.Nodes.OfType<CodeTreeLeaf>().Last().Text;
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