namespace CsLuaConverter.CodeTreeLuaVisitor.Expression
{
    using System.CodeDom.Compiler;
    using System.Linq;
    using CodeTree;
    using Microsoft.CodeAnalysis.CSharp;
    using Providers;
    using Providers.TypeKnowledgeRegistry;

    public class FalseLiteralExpressionVisitor : BaseVisitor
    {
        private readonly string value;

        public FalseLiteralExpressionVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.ExpectKind(0, SyntaxKind.TrueKeyword, SyntaxKind.FalseKeyword);
            this.value = ((CodeTreeLeaf) this.Branch.Nodes.Single()).Text;
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            textWriter.Write(this.value);
            providers.TypeKnowledgeRegistry.CurrentType = new TypeKnowledge(typeof(bool));
        }
    }
}