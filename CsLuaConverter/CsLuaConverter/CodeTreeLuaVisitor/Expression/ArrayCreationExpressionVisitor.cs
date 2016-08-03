namespace CsLuaConverter.CodeTreeLuaVisitor.Expression
{
    using CodeTree;
    using Microsoft.CodeAnalysis.CSharp;
    using Providers;
    using Type;

    public class ArrayCreationExpressionVisitor : BaseVisitor
    {
        private readonly ArrayTypeVisitor arrayType;
        private readonly ArrayInitializerExpressionVisitor initializer;

        public ArrayCreationExpressionVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.ExpectKind(0, SyntaxKind.NewKeyword);
            this.ExpectKind(1, SyntaxKind.ArrayType);
            this.ExpectKind(2, SyntaxKind.ArrayInitializerExpression);
            this.arrayType = (ArrayTypeVisitor) this.CreateVisitor(1);
            this.initializer = (ArrayInitializerExpressionVisitor) this.CreateVisitor(2);
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            textWriter.Write("(");
            this.arrayType.Visit(textWriter, providers);
            textWriter.Write(" % _M.DOT)");
            var currentType = providers.Context.CurrentType;
            this.initializer.Visit(textWriter, providers);
            providers.Context.CurrentType = currentType;
        }
    }
}