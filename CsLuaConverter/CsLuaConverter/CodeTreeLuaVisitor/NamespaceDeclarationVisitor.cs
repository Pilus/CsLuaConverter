namespace CsLuaConverter.CodeTreeLuaVisitor
{
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
        private BaseVisitor[] usingVisitors;
        
        public NamespaceDeclarationVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.CreateNameVisitor();
            this.CreateUsingVisitors();
            this.CreateElementVisitor();
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            this.usingVisitors.VisitAll(textWriter, providers);

            var state = providers.PartialElementState;
            var isFirstNamespace = state.IsFirst;
            var isLastNamespace = state.IsLast;

            var elements = this.elementVisitors.Where(v => v.GetNumOfGenerics() == state.NumberOfGenerics).ToArray();

            for (var index = 0; index < elements.Length; index++)
            {
                var elementVisitor = elements[index];

                state.IsFirst = isFirstNamespace && index == 0;
                state.IsLast = isLastNamespace && index == elements.Length - 1;
                elementVisitor.Visit(textWriter, providers);
            }
        }

        public void WriteFooter(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            var state = providers.PartialElementState;
            state.CurrentState = (int)ClassState.Footer;

            for (var index = 0; index < this.elementVisitors.Length; index++)
            {
                var elementVisitor = this.elementVisitors[index] as ClassDeclarationVisitor;

                if (elementVisitor != null)
                { 
                    state.IsFirst = index == 0;
                    state.IsLast = index == this.elementVisitors.Length - 1;

                    elementVisitor.Visit(textWriter, providers);
                }
            }
        }

        public void WriteExtensions(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            this.elementVisitors.OfType<ClassDeclarationVisitor>()
                .ToList()
                .ForEach(e => e.WriteExtensionMethods(textWriter, providers));
        }

        private void CreateNameVisitor()
        {
            this.ExpectKind(0, SyntaxKind.NamespaceKeyword);
            this.ExpectKind(1, SyntaxKind.QualifiedName, SyntaxKind.IdentifierName);
            this.nameVisitor = (INameVisitor)this.CreateVisitor(1);
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
            return this.nameVisitor.GetName();
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