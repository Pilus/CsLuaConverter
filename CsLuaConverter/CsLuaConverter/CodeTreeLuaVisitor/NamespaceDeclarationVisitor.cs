namespace CsLuaConverter.CodeTreeLuaVisitor
{
    using System.CodeDom.Compiler;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using CodeTree;
    using Elements;
    using Filters;
    using Microsoft.CodeAnalysis.CSharp;
    using Name;
    using Providers;

    public class NamespaceDeclarationVisitor : BaseVisitor
    {
        private INameVisitor nameVisitor;
        private IElementVisitor[] elementVisitors;
        public NamespaceDeclarationVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.CreateNameVisitor();
            this.CreateElementVisitor();
        }

        public override void Visit(IndentedTextWriter textWriter, IProviders providers)
        {
            
            providers.TypeProvider.SetCurrentNamespace(this.nameVisitor.GetName());

            this.CreateVisitorsAndVisitBranches(textWriter, providers, new KindFilter(SyntaxKind.UsingDirective));

            this.CreateVisitorsAndVisitBranches(textWriter, providers, new KindFilter(SyntaxKind.ClassDeclaration, SyntaxKind.InterfaceDeclaration, SyntaxKind.EnumDeclaration));
        }

        private void CreateNameVisitor()
        {
            this.ExpectKind(0, SyntaxKind.NamespaceKeyword);
            this.ExpectKind(1, new[] { SyntaxKind.QualifiedName, SyntaxKind.IdentifierName });
            this.nameVisitor = (INameVisitor)this.CreateVisitor(1);
        }

        private void CreateElementVisitor()
        {
            var elementVisitors = this.CreateVisitors(new KindFilter(SyntaxKind.ClassDeclaration, SyntaxKind.InterfaceDeclaration,
                SyntaxKind.EnumDeclaration)).OfType<IElementVisitor>().ToArray();

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
            return this.nameVisitor.GetName();
        }

        public string GetElementName()
        {
            return this.elementVisitors.First().GetName();
        }

        public int GetNumGenericsOfElement()
        {
            return this.elementVisitors.First().GetNumOfGenerics();
        }

        private static bool AreAllElementsIdentical(IEnumerable<object> e)
        {
            var a = e.ToArray();
            var first = a.First();
            return a.All(o => o == first);
        }
    }
}