﻿namespace CsLuaConverter.CodeTreeLuaVisitor
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
        private BaseVisitor[] usingVisitors;
        
        public NamespaceDeclarationVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.CreateNameVisitor();
            this.CreateUsingVisitors();
            this.CreateElementVisitor();
        }

        public override void Visit(IndentedTextWriter textWriter, IProviders providers)
        {
            
            providers.TypeProvider.SetCurrentNamespace(this.nameVisitor.GetName());

            this.usingVisitors.VisitAll(textWriter, providers);

            var state = providers.PartialElementState;
            var isFirstNamespace = state.IsFirst;
            var isLastNamespace = state.IsLast;

            for (var index = 0; index < this.elementVisitors.Length; index++)
            {
                var elementVisitor = this.elementVisitors[index];

                state.IsFirst = isFirstNamespace && index == 0;
                state.IsLast = isLastNamespace && index == this.elementVisitors.Length - 1;
                elementVisitor.Visit(textWriter, providers);
            }
        }

        public void WriteFooter(IndentedTextWriter textWriter, IProviders providers)
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

        private void CreateNameVisitor()
        {
            this.ExpectKind(0, SyntaxKind.NamespaceKeyword);
            this.ExpectKind(1, new[] { SyntaxKind.QualifiedName, SyntaxKind.IdentifierName });
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