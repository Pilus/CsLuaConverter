namespace CsLuaConverter.CodeTreeLuaVisitor.Expression
{
    using System.CodeDom.Compiler;
    using CodeTree;
    using Microsoft.CodeAnalysis.CSharp;
    using Name;
    using Providers;
    using Type;

    public class IsExpressionVisitor : BaseVisitor
    {
        private readonly BaseVisitor target;
        private readonly ITypeVisitor type;
        public IsExpressionVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.ExpectKind(1, SyntaxKind.IsKeyword);
            this.target = this.CreateVisitor(0);
            this.type = (ITypeVisitor)this.CreateVisitor(2);
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            this.type.WriteAsReference(textWriter, providers);
            textWriter.Write(".__is(");
            this.target.Visit(textWriter, providers);
            textWriter.Write(")");
        }
    }
}