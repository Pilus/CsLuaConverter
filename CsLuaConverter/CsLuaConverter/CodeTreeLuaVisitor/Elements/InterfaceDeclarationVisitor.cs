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

        public override void Visit(IIndentedTextWriterWrapper textWriter, IContext context)
        {
            TryActionAndWrapException(() =>
            {
                switch ((InterfaceState)(context.PartialElementState.CurrentState ?? 0))
                {
                    default:
                        this.WriteOpen(textWriter, context);
                        context.PartialElementState.NextState = (int)InterfaceState.Members;
                        break;
                    case InterfaceState.Members:
                        this.WriteMembers(textWriter, context);
                        context.PartialElementState.NextState = (int)InterfaceState.Close;
                        break;
                    case InterfaceState.Close:
                        this.WriteClose(textWriter, context);
                        context.PartialElementState.NextState = null;
                        break;
                }
            }, $"In visiting of interface {this.name}. State: {((InterfaceState)(context.PartialElementState.CurrentState ?? 0))}");
        }

        public string GetName()
        {
            return this.name;
        }

        public int GetNumOfGenerics()
        {
            return this.genericsVisitor?.GetNumElements() ?? 0;
        }


        private void WriteOpen(IIndentedTextWriterWrapper textWriter, IContext context)
        {
            if (!context.PartialElementState.IsFirst) return;
                
            textWriter.WriteLine("[{0}] = function(interactionElement, generics, staticValues)", this.GetNumOfGenerics());
            textWriter.Indent++;

            this.WriteGenericsMapping(textWriter, context);

            var symbol = context.SemanticModel.GetDeclaredSymbol(this.Branch.SyntaxNode as InterfaceDeclarationSyntax);
            var adaptor = context.SemanticAdaptor;

            textWriter.Write(
                "local typeObject = System.Type('{0}','{1}', nil, {2}, generics, nil, interactionElement, 'Interface',",
                adaptor.GetName(symbol), adaptor.GetFullNamespace(symbol), this.GetNumOfGenerics());
            context.SignatureWriter.WriteSignature(symbol, textWriter);
            textWriter.WriteLine(");");

            this.WriteImplements(textWriter, context);
            this.WriteAttributes(textWriter, context);
        }

        private void WriteClose(IIndentedTextWriterWrapper textWriter, IContext context)
        {
            if (!context.PartialElementState.IsLast) return;

            textWriter.WriteLine("return 'Interface', typeObject, getMembers, nil, nil, nil, nil, attributes;");

            textWriter.Indent--;
            textWriter.WriteLine("end,");
        }

        private void WriteGenericsMapping(IIndentedTextWriterWrapper textWriter, IContext context)
        {
            textWriter.Write("local genericsMapping = ");

            if (this.genericsVisitor != null)
            {
                textWriter.Write("{");
                this.genericsVisitor.Visit(textWriter, context);
                textWriter.WriteLine("};");
            }
            else
            {
                textWriter.WriteLine("{};");
            }
        }

        private void WriteImplements(IIndentedTextWriterWrapper textWriter, IContext context)
        {
            textWriter.WriteLine("local implements = {");
            textWriter.Indent++;

            var symbol = context.SemanticModel.GetDeclaredSymbol(this.Branch.SyntaxNode as InterfaceDeclarationSyntax);

            foreach (var interfaceType in symbol.Interfaces)
            {
                context.TypeReferenceWriter.WriteTypeReference(interfaceType, textWriter);
                textWriter.WriteLine(",");
            }

            textWriter.Indent--;
            textWriter.WriteLine("};");
            textWriter.WriteLine("typeObject.implements = implements;");
        }

        private void WriteAttributes(IIndentedTextWriterWrapper textWriter, IContext context)
        {
            this.attributeListVisitor?.Visit(textWriter, context);
        }

        private void WriteMembers(IIndentedTextWriterWrapper textWriter, IContext context)
        {
            if (context.PartialElementState.IsFirst)
            {
                textWriter.WriteLine("local getMembers = function()");
                textWriter.Indent++;
                textWriter.WriteLine("local members = {};");
                textWriter.WriteLine("_M.GAM(members, implements);");
            }

            this.members.VisitAll(textWriter, context);

            if (context.PartialElementState.IsLast)
            {
                textWriter.WriteLine("return members;");
                textWriter.Indent--;
                textWriter.WriteLine("end");
            }
        }
    }
}