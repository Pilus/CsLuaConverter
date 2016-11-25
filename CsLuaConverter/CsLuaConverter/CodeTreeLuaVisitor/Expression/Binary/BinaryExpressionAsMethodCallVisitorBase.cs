namespace CsLuaConverter.CodeTreeLuaVisitor.Expression.Binary
{
    using CodeTree;
    using CsLuaConverter.Context;
    using Microsoft.CodeAnalysis.CSharp;

    public class BinaryExpressionAsMethodCallVisitorBase : BinaryExpressionVisitorBase
    {
        private readonly IVisitor lhsVisitor;
        private readonly IVisitor rhsVisitor;
        private readonly string methodName;

        public BinaryExpressionAsMethodCallVisitorBase(CodeTreeBranch branch, SyntaxKind expectedKind, string methodName) : base(branch, SyntaxKind.EmptyStatement, "")
        {
            this.ExpectKind(1, expectedKind);
            this.lhsVisitor = this.CreateVisitor(0);
            this.rhsVisitor = this.CreateVisitor(2);
            this.methodName = methodName;
        }
        /*
        public override void Visit(IIndentedTextWriterWrapper textWriter, IContext context)
        {
            textWriter.Write($"{this.methodName}(");
            this.lhsVisitor.Visit(textWriter, context);

            textWriter.Write(", ");
            this.rhsVisitor.Visit(textWriter, context);
            textWriter.Write(")");
        } */
    }
}