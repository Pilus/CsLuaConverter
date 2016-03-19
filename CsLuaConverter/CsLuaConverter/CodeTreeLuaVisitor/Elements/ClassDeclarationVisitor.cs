namespace CsLuaConverter.CodeTreeLuaVisitor.Elements
{
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.Linq;
    using CodeTree;
    using Filters;
    using Lists;
    using Microsoft.CodeAnalysis.CSharp;
    using Name;
    using Providers;
    using Providers.TypeProvider;

    public class ClassDeclarationVisitor : BaseVisitor, IElementVisitor
    {
        private string name;
        private IListVisitor genericsVisitor;
        private List<ScopeElement> originalScope;

        public ClassDeclarationVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.CreateNameVisitor();
            this.CreateGenericsVisitor();
        }

        public override void Visit(IndentedTextWriter textWriter, IProviders providers)
        {
            TryActionAndWrapException(() =>
            {
                switch ((ClassState) (providers.PartialElementState.CurrentState ?? 0))
                {
                    default:
                        this.WriteOpen(textWriter, providers);
                        providers.PartialElementState.NextState = (int)ClassState.Close;
                        break;
                    case ClassState.Close:
                        this.WriteClose(textWriter, providers);
                        providers.PartialElementState.NextState = null;
                        break;
                }
            }, $"In visiting of class {this.name}. State: {((ClassState)(providers.PartialElementState.CurrentState ?? 0))}");
        }

        private void WriteOpen(IndentedTextWriter textWriter, IProviders providers)
        {
            if (providers.PartialElementState.IsFirst)
            {
                this.originalScope = providers.NameProvider.CloneScope();
                providers.NameProvider.AddAllInheritedMembersToScope(this.name);

                textWriter.WriteLine("[{0}] = function(interactionElement, generics, staticValues)", this.GetNumOfGenerics());
                textWriter.Indent++;
            }

            textWriter.WriteLine("-- X");
        }

        private void WriteClose(IndentedTextWriter textWriter, IProviders providers)
        {
            if (providers.PartialElementState.IsLast)
            {
                textWriter.Indent--;
                textWriter.WriteLine("end,");
            }

            if (providers.PartialElementState.IsFirst)
            {
                providers.NameProvider.SetScope(this.originalScope);
            }
        }



        public string GetName()
        {
            return this.name;
        }

        private void CreateNameVisitor()
        {
            var accessorNodes = this.GetFilteredNodes(new KindRangeFilter(null, SyntaxKind.ClassKeyword));
            this.ExpectKind(accessorNodes.Length, SyntaxKind.ClassKeyword);
            this.ExpectKind(accessorNodes.Length + 1, SyntaxKind.IdentifierToken);
            this.name = ((CodeTreeLeaf) this.Branch.Nodes[accessorNodes.Length + 1]).Text;
        }

        private void CreateGenericsVisitor()
        {
            this.genericsVisitor = (IListVisitor)this.CreateVisitors(new KindFilter(SyntaxKind.TypeArgumentList)).SingleOrDefault();
        }

        public int GetNumOfGenerics()
        {
            return this.genericsVisitor?.GetNumElements() ?? 0;
        }
    }
}