namespace CsLuaConverter.CodeTreeLuaVisitor.Expression.Literal
{
    using CodeTree;
    using Providers;
    using Providers.TypeKnowledgeRegistry;

    public class StringLiteralExpressionVisitor : BaseVisitor
    {
        private readonly string text;

        public StringLiteralExpressionVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.text = ((CodeTreeLeaf)this.Branch.Nodes[0]).Text;
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            var txt = this.text;

            if (txt.StartsWith("@"))
            {
                txt = "[[" + txt.Substring(2, txt.Length - 3) + "]]";
            }

            textWriter.Write(txt);

            providers.TypeKnowledgeRegistry.CurrentType = new TypeKnowledge(typeof(string));
        }
    }
}