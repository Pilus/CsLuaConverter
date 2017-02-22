namespace CsLuaConverter.CodeTreeLuaVisitor
{
    using System.Collections.Generic;
    using System.Linq;
    using CodeTree;
    using CsLuaSyntaxTranslator;
    using CsLuaSyntaxTranslator.Context;
    using CsLuaSyntaxTranslator.SyntaxExtensions;
    using Elements;
    using Filters;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;
    using Name;

    public class NamespaceDeclarationVisitor : BaseVisitor
    {
        //private INameVisitor nameVisitor;
        private IElementVisitor[] elementVisitors;
        private BaseVisitor[] usingVisitors;
        
        public NamespaceDeclarationVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.CreateNameVisitor();
            this.CreateUsingVisitors();
            this.CreateElementVisitor();
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IContext context)
        {
            var syntax = this.Branch.SyntaxNode as NamespaceDeclarationSyntax;

            syntax.Write(textWriter, context);
        }

        public void WriteFooter(IIndentedTextWriterWrapper textWriter, IContext context)
        {
            var state = context.PartialElementState;
            state.CurrentState = (int)ClassState.Footer;

            for (var index = 0; index < this.elementVisitors.Length; index++)
            {
                var elementVisitor = this.elementVisitors[index] as ClassDeclarationVisitor;

                if (elementVisitor != null)
                { 
                    state.IsFirst = index == 0;
                    state.IsLast = index == this.elementVisitors.Length - 1;

                    elementVisitor.Visit(textWriter, context);
                }
            }
        }

        private void CreateNameVisitor()
        {
            this.ExpectKind(0, SyntaxKind.NamespaceKeyword);
            this.ExpectKind(1, SyntaxKind.QualifiedName, SyntaxKind.IdentifierName);
            //this.nameVisitor = (INameVisitor)this.CreateVisitor(1);
        }

        private void CreateUsingVisitors()
        {
            this.usingVisitors = this.CreateVisitors(new KindFilter(SyntaxKind.UsingDirective));
        }

        private void CreateElementVisitor()
        {
            var elementVisitors = this.CreateVisitors().OfType<IElementVisitor>().ToArray();

            if (elementVisitors.Length == 0)
            {
                throw new VisitorException("Namespace with no element detected. Note that structs are not supported.");
            }

            if (elementVisitors.Length > 1)
            {
                if (!AreAllElementsIdentical(elementVisitors.Select(v => v.GetName())))
                {
                    throw new VisitorException("File with multiple elements with different name is not supported.");
                }
            }

            this.elementVisitors = elementVisitors;
        }

        public string[] GetNamespaceName()
        {
            var syntax = (NamespaceDeclarationSyntax)this.Branch.SyntaxNode;
            return GetName(syntax.Name).ToArray();
        }

        private static List<string> GetName(NameSyntax syntax)
        {
            if (syntax is IdentifierNameSyntax)
            {
                return new List<string>(new[] {((IdentifierNameSyntax) syntax).Identifier.Text});
            }

            if (syntax is QualifiedNameSyntax)
            {
                var name = (QualifiedNameSyntax)syntax;
                var list = GetName(name.Left);
                list.Add(name.Right.Identifier.Text);
                return list;
            }

            throw new System.NotImplementedException();
        }

        public string GetElementName()
        {
            return this.elementVisitors.First().GetName();
        }

        public int[] GetNumGenericsOfElement()
        {
            return this.elementVisitors.Select(v => v.GetNumOfGenerics()).ToArray();
        }

        private static bool AreAllElementsIdentical(IEnumerable<object> e)
        {
            var a = e.ToArray();
            var first = a.First();
            return a.All(o => o == first);
        }
    }
}