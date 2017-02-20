namespace CsLuaConverter.CodeTreeLuaVisitor.Member
{
    using System.Linq;
    using CodeTree;
    using CsLuaConverter.Context;
    using CsLuaConverter.SyntaxExtensions;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;
    using SyntaxNodeExtensions = CsLuaConverter.SyntaxExtensions.SyntaxNodeExtensions;

    public class VariableDeclaratorVisitor : BaseVisitor
    {
        private readonly string name;
        private readonly BaseVisitor valueVisitor;

        public VariableDeclaratorVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.ExpectKind(0, SyntaxKind.IdentifierToken);
            this.name = ((CodeTreeLeaf) this.Branch.Nodes.First()).Text;

            if (this.Branch.Nodes.Length > 1)
            {
                this.ExpectKind(1, SyntaxKind.EqualsValueClause);
                this.valueVisitor = this.CreateVisitor(1);
            }
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IContext context)
        {
            //textWriter.Write(this.name);
            //this.valueVisitor?.Visit(textWriter, context);
            (this.Branch.SyntaxNode as VariableDeclaratorSyntax).Write(textWriter, context);
        }

        public string GetName()
        {
            return this.name;
        }
    }
}