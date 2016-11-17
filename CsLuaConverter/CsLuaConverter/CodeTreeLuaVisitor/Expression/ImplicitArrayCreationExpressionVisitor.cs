namespace CsLuaConverter.CodeTreeLuaVisitor.Expression
{
    using System.Linq;
    using CodeTree;
    using Filters;
    using Microsoft.CodeAnalysis.CSharp;
    using Providers;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    public class ImplicitArrayCreationExpressionVisitor : BaseVisitor
    {
        private readonly ArrayInitializerExpressionVisitor creationExpression;
        public ImplicitArrayCreationExpressionVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.creationExpression = (ArrayInitializerExpressionVisitor)this.CreateVisitors(new KindFilter(SyntaxKind.ArrayInitializerExpression)).Single();
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IContext context)
        {
            var typeInfo = context.SemanticModel.GetTypeInfo(this.Branch.SyntaxNode);
            textWriter.Write("(");
            context.TypeReferenceWriter.WriteInteractionElementReference(typeInfo.Type, textWriter);
            textWriter.Write("._C_0_0() % _M.DOT)");
            this.creationExpression.Visit(textWriter, context);
        }
    }
}