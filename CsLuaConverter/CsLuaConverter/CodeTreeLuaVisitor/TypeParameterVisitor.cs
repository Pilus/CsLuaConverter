namespace CsLuaConverter.CodeTreeLuaVisitor
{
    using System.CodeDom.Compiler;
    using CodeTree;
    using Microsoft.CodeAnalysis.CSharp;
    using Providers;

    public class TypeParameterVisitor : BaseVisitor
    {
        public string Name;

        public TypeParameterVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.ExpectKind(0, SyntaxKind.IdentifierToken);
            this.Name = ((CodeTreeLeaf) this.Branch.Nodes[0]).Text;
        }

        public override void Visit(IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.Write(this.Name);
        }
    }
}