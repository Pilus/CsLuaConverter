namespace CsLuaConverter.CodeTreeLuaVisitor.Elements
{
    using System.Linq;
    using System.Reflection.PortableExecutable;

    using Attribute;
    using CodeTree;
    using Expression;
    using Filters;
    using Lists;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    using Providers;
    using Providers.GenericsRegistry;
    using Providers.TypeKnowledgeRegistry;
    using Providers.TypeProvider;

    public class InterfaceDeclarationVisitor : BaseVisitor, IElementVisitor
    {
        private readonly string name;
        private readonly TypeParameterListVisitor genericsVisitor;
        private readonly BaseVisitor[] members;
        private AttributeListVisitor attributeListVisitor;
        private readonly BaseListVisitor baseList;

        public InterfaceDeclarationVisitor(CodeTreeBranch branch) : base(branch)
        {
            var accessorNodes = this.GetFilteredNodes(new KindRangeFilter(null, SyntaxKind.InterfaceKeyword));
            this.ExpectKind(accessorNodes.Length, SyntaxKind.InterfaceKeyword);
            this.ExpectKind(accessorNodes.Length + 1, SyntaxKind.IdentifierToken);
            this.name = ((CodeTreeLeaf)this.Branch.Nodes[accessorNodes.Length + 1]).Text;
            this.genericsVisitor = (TypeParameterListVisitor)this.CreateVisitors(new KindFilter(SyntaxKind.TypeParameterList)).SingleOrDefault();
            this.baseList = (BaseListVisitor)this.CreateVisitors(new KindFilter(SyntaxKind.BaseList)).SingleOrDefault();
            this.attributeListVisitor =
                this.CreateVisitors(new KindFilter(SyntaxKind.AttributeList))
                    .Select(v => (AttributeListVisitor)v)
                    .SingleOrDefault();
            this.members = this.CreateVisitors(new KindRangeFilter(SyntaxKind.OpenBraceToken, SyntaxKind.CloseBraceToken));
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            TryActionAndWrapException(() =>
            {
                switch ((InterfaceState)(providers.PartialElementState.CurrentState ?? 0))
                {
                    default:
                        this.WriteOpen(textWriter, providers);
                        providers.PartialElementState.NextState = (int)InterfaceState.Members;
                        break;
                    case InterfaceState.Members:
                        this.WriteMembers(textWriter, providers);
                        providers.PartialElementState.NextState = (int)InterfaceState.Close;
                        break;
                    case InterfaceState.Close:
                        this.WriteClose(textWriter, providers);
                        providers.PartialElementState.NextState = null;
                        break;
                }
            }, $"In visiting of interface {this.name}. State: {((InterfaceState)(providers.PartialElementState.CurrentState ?? 0))}");
        }

        public string GetName()
        {
            return this.name;
        }

        public int GetNumOfGenerics()
        {
            return this.genericsVisitor?.GetNumElements() ?? 0;
        }


        private void WriteOpen(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            if (!providers.PartialElementState.IsFirst) return;
                
            textWriter.WriteLine("[{0}] = function(interactionElement, generics, staticValues)", this.GetNumOfGenerics());
            textWriter.Indent++;

            this.WriteGenericsMapping(textWriter, providers);

            var symbol = providers.SemanticModel.GetDeclaredSymbol(this.Branch.SyntaxNode as InterfaceDeclarationSyntax);
            var adaptor = providers.SemanticAdaptor;

            textWriter.Write(
                "local typeObject = System.Type('{0}','{1}', nil, {2}, generics, nil, interactionElement, 'Interface',",
                adaptor.GetName(symbol), adaptor.GetFullNamespace(symbol), this.GetNumOfGenerics());
            providers.SignatureWriter.WriteSignature(symbol, textWriter);
            textWriter.WriteLine(");");

            this.WriteImplements(textWriter, providers);
            this.WriteAttributes(textWriter, providers);
        }

        private void WriteClose(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            if (!providers.PartialElementState.IsLast) return;

            textWriter.WriteLine("return 'Interface', typeObject, getMembers, nil, nil, nil, nil, attributes;");

            textWriter.Indent--;
            textWriter.WriteLine("end,");
        }

        private void RegisterGenerics(IProviders providers)
        {
            if (this.genericsVisitor == null)
            {
                return;
            }

            var classTypeResult = providers.TypeProvider.LookupType(new []{this.name}, this.GetNumOfGenerics());
            var generics = classTypeResult.TypeObject.GetGenericArguments();

            foreach (var genericName in this.genericsVisitor.GetNames())
            {
                providers.GenericsRegistry.SetGenerics(
                    genericName, 
                    GenericScope.Class, 
                    generics.Single(t => t.Name == genericName), 
                    typeof(object));
            }
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

        private void WriteImplements(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            textWriter.WriteLine("local implements = {");
            textWriter.Indent++;

            var symbol = providers.SemanticModel.GetDeclaredSymbol(this.Branch.SyntaxNode as InterfaceDeclarationSyntax);

            foreach (var interfaceType in symbol.Interfaces)
            {
                providers.TypeReferenceWriter.WriteTypeReference(interfaceType, textWriter);
                textWriter.WriteLine(",");
            }

            textWriter.Indent--;
            textWriter.WriteLine("};");
            textWriter.WriteLine("typeObject.implements = implements;");
        }

        private void WriteAttributes(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            this.attributeListVisitor?.Visit(textWriter, providers);
        }

        private void WriteMembers(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            if (providers.PartialElementState.IsFirst)
            {
                textWriter.WriteLine("local getMembers = function()");
                textWriter.Indent++;
                textWriter.WriteLine("local members = {};");
                textWriter.WriteLine("_M.GAM(members, implements);");
            }

            this.members.VisitAll(textWriter, providers);

            if (providers.PartialElementState.IsLast)
            {
                textWriter.WriteLine("return members;");
                textWriter.Indent--;
                textWriter.WriteLine("end");
            }
        }
    }
}