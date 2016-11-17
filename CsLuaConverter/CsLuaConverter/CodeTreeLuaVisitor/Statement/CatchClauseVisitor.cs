namespace CsLuaConverter.CodeTreeLuaVisitor.Statement
{
    using CodeTree;
    using CsLuaConverter.Context;
    using Microsoft.CodeAnalysis.CSharp;

    public class CatchClauseVisitor : BaseVisitor
    {
        private readonly CatchDeclarationVisitor declaration;
        private readonly BlockVisitor block;
        public CatchClauseVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.ExpectKind(0, SyntaxKind.CatchKeyword);
            var i = 1;
            if (this.IsKind(i, SyntaxKind.CatchDeclaration))
            {
                this.declaration = (CatchDeclarationVisitor)this.CreateVisitor(i);
                i++;
            }
            
            this.ExpectKind(i, SyntaxKind.Block);
            this.block = (BlockVisitor)this.CreateVisitor(i);
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IContext context)
        {
            textWriter.WriteLine("{");
            textWriter.Indent++;

            if (this.declaration != null)
            {
                this.declaration.Visit(textWriter, context);
            }
            else
            {
                textWriter.WriteLine("func = function()");
            }

            this.block.Visit(textWriter, context);
            
            textWriter.WriteLine("end,");
            
            textWriter.Indent--;
            textWriter.WriteLine("},");
        }
    }
}