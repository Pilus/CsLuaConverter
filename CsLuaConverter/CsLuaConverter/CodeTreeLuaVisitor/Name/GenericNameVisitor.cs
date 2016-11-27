namespace CsLuaConverter.CodeTreeLuaVisitor.Name
{
    using System.Linq;
    using CodeTree;

    using CsLuaConverter.Context;
    using Lists;

    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    public class GenericNameVisitor : BaseVisitor, INameVisitor
    {
        private readonly string name;
        private readonly TypeArgumentListVisitor argumentListVisitor;

        public GenericNameVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.ExpectKind(0, SyntaxKind.IdentifierToken);
            this.name = ((CodeTreeLeaf) this.Branch.Nodes[0]).Text;
            this.argumentListVisitor = (TypeArgumentListVisitor) this.CreateVisitor(1);
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IContext context)
        {
            var hasInvocationExpressionParent = this.Branch.SyntaxNode.Ancestors().OfType<InvocationExpressionSyntax>().Any();
            if (hasInvocationExpressionParent)
            {
                textWriter.Write(this.name);
                return;
            }

            textWriter.Write(this.name);
            textWriter.Write("[");
            this.argumentListVisitor.Visit(textWriter, context);
            textWriter.Write("]");
        }

        public string[] GetName()
        {
            return new[] {this.name};
        }
    }
}