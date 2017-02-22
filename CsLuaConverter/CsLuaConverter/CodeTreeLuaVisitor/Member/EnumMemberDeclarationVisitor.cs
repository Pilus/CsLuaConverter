namespace CsLuaConverter.CodeTreeLuaVisitor.Member
{
    using CodeTree;
    using CsLuaSyntaxTranslator;
    using CsLuaSyntaxTranslator.Context;
    using Microsoft.CodeAnalysis.CSharp;

    public class EnumMemberDeclarationVisitor : BaseVisitor
    {
        private readonly string index;

        public EnumMemberDeclarationVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.ExpectKind(0, SyntaxKind.IdentifierToken);
            this.index = ((CodeTreeLeaf) this.Branch.Nodes[0]).Text;
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IContext context)
        {
            textWriter.Write($"[\"{this.index}\"] = ");
            this.WriteValue(textWriter);
        }

        public void WriteAsDefault(IIndentedTextWriterWrapper textWriter, IContext context)
        {
            textWriter.Write("__default = ");
            this.WriteValue(textWriter);
        }

        private void WriteValue(IIndentedTextWriterWrapper textWriter)
        {
            textWriter.Write($"\"{this.index}\"");
        }
    }
}