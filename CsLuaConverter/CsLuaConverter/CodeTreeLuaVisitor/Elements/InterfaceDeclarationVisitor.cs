namespace CsLuaConverter.CodeTreeLuaVisitor.Elements
{
    using System.Linq;
    using CodeTree;
    using Filters;
    using Lists;
    using Microsoft.CodeAnalysis.CSharp;
    using Providers;
    using Providers.GenericsRegistry;

    public class InterfaceDeclarationVisitor : BaseVisitor, IElementVisitor
    {
        private string name;
        private TypeParameterListVisitor genericsVisitor;

        public InterfaceDeclarationVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.CreateNameVisitor();
            this.CreateGenericsVisitor();
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            this.WriteOpen(textWriter, providers);
        }

        public string GetName()
        {
            return this.name;
        }

        public int GetNumOfGenerics()
        {
            return this.genericsVisitor?.GetNumElements() ?? 0;
        }

        private void CreateNameVisitor()
        {
            var accessorNodes = this.GetFilteredNodes(new KindRangeFilter(null, SyntaxKind.InterfaceKeyword));
            this.ExpectKind(accessorNodes.Length, SyntaxKind.InterfaceKeyword);
            this.ExpectKind(accessorNodes.Length + 1, SyntaxKind.IdentifierToken);
            this.name = ((CodeTreeLeaf)this.Branch.Nodes[accessorNodes.Length + 1]).Text;
        }

        private void CreateGenericsVisitor()
        {
            this.genericsVisitor = (TypeParameterListVisitor)this.CreateVisitors(new KindFilter(SyntaxKind.TypeArgumentList)).SingleOrDefault();
        }


        private void WriteOpen(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            textWriter.WriteLine("[{0}] = function(interactionElement, generics, staticValues)", this.GetNumOfGenerics());
            textWriter.Indent++;

            this.RegisterGenerics(providers);
            this.WriteGenericsMapping(textWriter, providers);

            var typeObject = providers.TypeProvider.LookupType(this.name);

            textWriter.WriteLine(
                "local typeObject = System.Type('{0}','{1}', nil, {2}, generics, nil, interactionElement, 'Interface');",
                typeObject.Name, typeObject.Namespace, this.GetNumOfGenerics());

            // WriteImplements(element, textWriter, providers);
            // WriteAttributes(attributes, textWriter, providers);
            // WriteMembers(elements, textWriter, providers);

            textWriter.WriteLine("return 'Interface', typeObject, memberProvider, nil, nil;");

            textWriter.Indent--;
            textWriter.WriteLine("end,");

            providers.GenericsRegistry.ClearScope(GenericScope.Class);
        }

        private void RegisterGenerics(IProviders providers)
        {
            if (this.genericsVisitor == null)
            {
                return;
            }

            providers.GenericsRegistry.SetGenerics(this.genericsVisitor.GetNames(), GenericScope.Class);
        }

        private void WriteGenericsMapping(IIndentedTextWriterWrapper textWriter, IProviders providers)
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
    }
}