namespace CsLuaConverter.CodeTreeLuaVisitor.Expression
{
    using CodeTree;
    using CsLuaConverter.Context;
    using Filters;
    using Microsoft.CodeAnalysis.CSharp;

    public class ObjectInitializerExpressionVisitor : BaseVisitor
    {
        private readonly BaseVisitor[] innerVisitors;

        public ObjectInitializerExpressionVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.ExpectKind(0, SyntaxKind.OpenBraceToken);
            this.ExpectKind(this.Branch.Nodes.Length - 1, SyntaxKind.CloseBraceToken);
            this.innerVisitors = this.CreateVisitors(new KindRangeFilter(SyntaxKind.OpenBraceToken, SyntaxKind.CloseBraceToken));
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IContext context)
        {
            textWriter.Write(".__Initialize({");

            if (this.innerVisitors.Length > 1)
                textWriter.WriteLine();

            textWriter.Indent++;
            this.innerVisitors.VisitAll(textWriter, context, () =>
            {
                textWriter.WriteLine(",");
            });
            textWriter.Indent--;
            
            if (this.innerVisitors.Length > 1)
                textWriter.WriteLine();

            textWriter.Write("})");
        }
    }
}