namespace CsLuaConverter.CodeTreeLuaVisitor.Type
{
    using System.Linq;

    using CsLuaConverter.CodeTree;
    using CsLuaConverter.CodeTreeLuaVisitor.Filters;
    using CsLuaConverter.Providers;
    using CsLuaConverter.Providers.TypeKnowledgeRegistry;

    using Microsoft.CodeAnalysis.CSharp;

    public class ArrayRankSpecifierVisitor : BaseVisitor
    {
        private readonly IVisitor rankVisitor;
        public ArrayRankSpecifierVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.rankVisitor =
                this.CreateVisitors(new KindRangeFilter(SyntaxKind.OpenBracketToken, SyntaxKind.CloseBracketToken))
                    .SingleOrDefault();
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            if (this.rankVisitor == null)
            {
                textWriter.Write("._C_0_0()");
            }
            else
            {
                textWriter.Write("._C_0_2112(");
                this.rankVisitor.Visit(textWriter, providers);
                textWriter.Write(")");
            }
        }
    }
}