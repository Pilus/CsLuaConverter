namespace CsLuaConverter.CodeTreeLuaVisitor.Member
{
    using System;
    using System.Linq;
    using Accessor;
    using CodeTree;
    using Filters;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    using Providers;
    using Type;

    public class PropertyDeclarationVisitor : BaseVisitor
    {
        private readonly string name;
        private readonly Scope scope;
        private readonly bool isStatic;
        private readonly ITypeVisitor type;
        private readonly AccessorListVisitor accessorList;

        public PropertyDeclarationVisitor(CodeTreeBranch branch) : base(branch)
        {
            var totalNodes = this.Branch.Nodes.Length;
            var accessorNodes = this.Branch.Nodes.Take(totalNodes - 3).ToArray();

            var scopeValue =
                ((CodeTreeLeaf)(new KindFilter(SyntaxKind.PrivateKeyword, SyntaxKind.PublicKeyword,
                    SyntaxKind.ProtectedKeyword, SyntaxKind.InternalKeyword).Filter(accessorNodes)).SingleOrDefault())?.Text;
            this.scope = scopeValue != null ? (Scope)Enum.Parse(typeof(Scope), scopeValue, true) : Scope.Public;

            this.isStatic = accessorNodes.Any(n => n.Kind.Equals(SyntaxKind.StaticKeyword));

            this.type = (ITypeVisitor) this.CreateVisitor(totalNodes - 3);

            this.ExpectKind(totalNodes - 2, SyntaxKind.IdentifierToken);
            this.name = ((CodeTreeLeaf) this.Branch.Nodes[totalNodes - 2]).Text;

            this.accessorList = (AccessorListVisitor) this.CreateVisitor(totalNodes - 1);
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            var symbol = providers.SemanticModel.GetDeclaredSymbol(this.Branch.SyntaxNode as PropertyDeclarationSyntax);

            textWriter.WriteLine("_M.IM(members, '{0}',{{", this.name);
            textWriter.Indent++;
            textWriter.WriteLine("level = typeObject.Level,");
            textWriter.WriteLine("memberType = '{0}',", this.accessorList.IsAutoProperty() ? "AutoProperty" : "Property");
            textWriter.WriteLine("scope = '{0}',", this.scope);
            textWriter.WriteLine("static = {0},", this.isStatic.ToString().ToLower());
            textWriter.Write("returnType = ");
            providers.TypeReferenceWriter.WriteTypeReference(symbol.Type, textWriter);
            textWriter.WriteLine(";");

            //providers.Context.CurrentType = this.type.GetType(providers);
            this.accessorList.Visit(textWriter, providers);
            textWriter.Indent--;
            textWriter.WriteLine("});");
        }

        public void WriteDefaultValue(IIndentedTextWriterWrapper textWriter, IProviders providers, bool isStaticFilter = false)
        {
            if (this.isStatic != isStaticFilter)
            {
                return;
            }

            var symbol = providers.SemanticModel.GetDeclaredSymbol(this.Branch.SyntaxNode as PropertyDeclarationSyntax);

            textWriter.Write($"{this.name} = _M.DV(");
            providers.TypeReferenceWriter.WriteTypeReference(symbol.Type, textWriter);
            textWriter.WriteLine("),");
        }

        public void WriteInitializeValue(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            textWriter.WriteLine($"if not(values.{this.name} == nil) then element[typeObject.Level].{this.name} = values.{this.name}; end");
        }
    }
}