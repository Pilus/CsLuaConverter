namespace CsLuaConverter.CodeTreeLuaVisitor.Elements
{
    using CodeTree;
    using CsLuaConverter.Context;
    using CsLuaConverter.SyntaxExtensions;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    public class ClassDeclarationVisitor : SyntaxVisitorBase<ClassDeclarationSyntax>, IElementVisitor
    {
        
        public ClassDeclarationVisitor(CodeTreeBranch branch) : base(branch)
        {
            
        }

        public ClassDeclarationVisitor(ClassDeclarationSyntax syntax) : base(syntax)
        {
        }


        public override void Visit(IIndentedTextWriterWrapper textWriter, IContext context)
        {
            var syntax = this.Branch.SyntaxNode as ClassDeclarationSyntax;

            syntax.Write(textWriter, context);
        }


        public string GetName()
        {
            return this.Syntax.Identifier.Text;
        }

        public int GetNumOfGenerics()
        {
            return this.Syntax.TypeParameterList?.Parameters.Count ?? 0;
        }
    }
}