namespace CsLuaConverter.CodeTreeLuaVisitor
{
    using System;
    using System.CodeDom.Compiler;
    using System.Linq;
    using CodeTree;
    using Filters;
    using Microsoft.CodeAnalysis.CSharp;
    using Providers;
    using Type;

    public class PropertyDeclarationVisitor : BaseVisitor
    {
        private readonly string name;
        private readonly Scope scope;
        private readonly bool isStatic;
        private readonly ITypeVisitor type;

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

            // TODO: Create accessor visitor
        }

        public override void Visit(IndentedTextWriter textWriter, IProviders providers)
        {
            throw new System.NotImplementedException();
        }

        public void WriteDefaultValue(IndentedTextWriter textWriter, IProviders providers, bool isStaticFilter = false)
        {
            if (this.isStatic != isStaticFilter)
            {
                return;
            }

            textWriter.Write($"{this.name} = _M.DV(");
            this.type.WriteAsType(textWriter, providers);
            textWriter.WriteLine("),");
        }

        public void WriteInitializeValue(IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.WriteLine($"if not(values.{this.name} == nil) then element[typeObject.Level].{this.name} = values.{this.name}; end");
        }
    }
}