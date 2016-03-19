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
    using Providers.GenericsRegistry;
    using Providers.TypeProvider;

    public class ClassDeclarationVisitor : BaseVisitor, IElementVisitor
    {
        private string name;
        private TypeParameterListVisitor genericsVisitor;
        private List<ScopeElement> originalScope;
        private BaseListVisitor baseListVisitor;

        public ClassDeclarationVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.CreateVisitors();
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

                this.RegisterGenerics(providers);
                this.WriteGenericsMapping(textWriter, providers);
                this.WriteTypeGeneration(textWriter, providers);
                this.WriteBaseInheritance(textWriter, providers);
            }
        }

        private void WriteGenericsMapping(IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.Write("local genericsMapping = ");

            if (this.genericsVisitor != null)
            {
                textWriter.Write("{");
                this.genericsVisitor.Visit(textWriter, providers);
                textWriter.WriteLine("};");
            }
            else
            {
                textWriter.WriteLine("{};");
            }
        }

        private void WriteTypeGeneration(IndentedTextWriter textWriter, IProviders providers)
        {
            var typeObject = providers.TypeProvider.LookupType(this.name);
            textWriter.WriteLine(
                "local typeObject = System.Type('{0}','{1}', nil, {2}, generics, nil, interactionElement);",
                typeObject.Name, typeObject.Namespace, this.genericsVisitor?.GetNumElements() ?? 0);
        }

        private void WriteBaseInheritance(IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.Write("local baseTypeObject, getBaseMembers, baseConstructors, baseElementGenerator, implements, baseInitialize = ");

            if (this.baseListVisitor?.WriteInteractiveObjectRefOfFirstTypeIfClass(textWriter, providers) != true)
            {
                textWriter.Write("System.Object");
            }
            
            textWriter.WriteLine(".__meta(staticValues);");
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

        private void CreateVisitors()
        {
            var accessorNodes = this.GetFilteredNodes(new KindRangeFilter(null, SyntaxKind.ClassKeyword));
            this.ExpectKind(accessorNodes.Length, SyntaxKind.ClassKeyword);
            this.ExpectKind(accessorNodes.Length + 1, SyntaxKind.IdentifierToken);
            this.name = ((CodeTreeLeaf) this.Branch.Nodes[accessorNodes.Length + 1]).Text;
            this.genericsVisitor = (TypeParameterListVisitor)this.CreateVisitors(new KindFilter(SyntaxKind.TypeParameterList)).SingleOrDefault();
            this.baseListVisitor = (BaseListVisitor)this.CreateVisitors(new KindFilter(SyntaxKind.BaseList)).SingleOrDefault();
        }

        public int GetNumOfGenerics()
        {
            return this.genericsVisitor?.GetNumElements() ?? 0;
        }

        private void RegisterGenerics(IProviders providers)
        {
            if (this.genericsVisitor == null)
            {
                return;
            }

            providers.GenericsRegistry.SetGenerics(this.genericsVisitor.GetNames(), GenericScope.Class);
        }
    }
}