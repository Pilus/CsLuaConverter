namespace CsLuaConverter.CodeTreeLuaVisitor.Accessor
{
    using System.Linq;
    using CodeTree;
    using CsLuaConverter.Context;
    using Filters;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    public class AccessorDeclarationVisitor : SyntaxVisitorBase<AccessorDeclarationSyntax>, IAccessor
    {
        public AccessorDeclarationVisitor(CodeTreeBranch branch) : base(branch)
        {

        }

        public AccessorDeclarationVisitor(AccessorDeclarationSyntax syntax) : base(syntax)
        {
            
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IContext context)
        {
            Visit(this.Syntax, textWriter, context);
        }

        public static void Visit(AccessorDeclarationSyntax syntax, IIndentedTextWriterWrapper textWriter, IContext context)
        {
            if (syntax.Body == null)
            {
                return;
            }

            textWriter.Write(syntax.Keyword.Text);
            textWriter.Write(" = function(element");

            var indexerDeclaration = syntax.Parent.Parent as IndexerDeclarationSyntax;
            if (indexerDeclaration != null)
            {
                textWriter.Write(", ");
                VisitAllNodes(indexerDeclaration.ParameterList.Parameters, textWriter, context, () => textWriter.Write(", "));
            }

            if (syntax.Keyword.Kind() == SyntaxKind.SetKeyword)
            {
                textWriter.Write(", value");
            }

            textWriter.WriteLine(")");
            VisitNode(syntax.Body, textWriter, context);
            textWriter.WriteLine("end,");
        }

        public bool IsAutoProperty()
        {
            throw new System.NotImplementedException();
        }
    }
}