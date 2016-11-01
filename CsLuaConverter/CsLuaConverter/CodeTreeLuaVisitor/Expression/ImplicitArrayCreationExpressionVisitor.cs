namespace CsLuaConverter.CodeTreeLuaVisitor.Expression
{
    using System.Linq;
    using CodeTree;
    using Filters;
    using Microsoft.CodeAnalysis.CSharp;
    using Providers;
    using Microsoft.CodeAnalysis;
    public class ImplicitArrayCreationExpressionVisitor : BaseVisitor
    {
        private readonly ArrayInitializerExpressionVisitor creationExpression;
        public ImplicitArrayCreationExpressionVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.creationExpression = (ArrayInitializerExpressionVisitor)this.CreateVisitors(new KindFilter(SyntaxKind.ArrayInitializerExpression)).Single();
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            textWriter.Write("(");
            var symbol = providers.SemanticModel.GetSymbolInfo(this.Branch.SyntaxNode);
            /// arrayType.WriteAsReference(textWriter, providers);
            textWriter.Write("._C_0_0()%_M.DOT)");
            this.creationExpression.Visit(textWriter, providers);
        }
    }
}