namespace CsLuaConverter.CodeTreeLuaVisitor
{
    using System;
    using System.CodeDom.Compiler;
    using System.Linq;
    using CodeTree;
    using Filters;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;
    using Providers;

    public class FieldDeclarationVisitor : BaseVisitor
    {

        private readonly VariableDeclarationVisitor variableVisitor;

        public bool IsStatic { get; private set; }

        public bool IsConst { get; private set; }

        public Scope Scope { get; private set; }

        public FieldDeclarationVisitor(CodeTreeBranch branch) : base(branch)
        {
            var accessorNodes = this.GetFilteredNodes(new KindRangeFilter(null, SyntaxKind.VariableDeclaration));
            var scopeValue =
                ((CodeTreeLeaf) (new KindFilter(SyntaxKind.PrivateKeyword, SyntaxKind.PublicKeyword,
                    SyntaxKind.ProtectedKeyword, SyntaxKind.InternalKeyword).Filter(accessorNodes)).SingleOrDefault())?.Text;
            this.Scope = scopeValue != null ? (Scope) Enum.Parse(typeof (Scope), scopeValue, true) : Scope.Public;

            this.IsStatic = accessorNodes.Any(n => n.Kind.Equals(SyntaxKind.StaticKeyword));

            this.IsConst = accessorNodes.Any(n => n.Kind.Equals(SyntaxKind.ConstKeyword));

            this.variableVisitor = (VariableDeclarationVisitor) this.CreateVisitor(accessorNodes.Length);
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            textWriter.WriteLine("_M.IM(members, '{0}', {{", this.variableVisitor.GetName());
            textWriter.Indent++;
            textWriter.WriteLine("level = typeObject.Level,");
            textWriter.WriteLine("memberType = 'Field',");
            textWriter.WriteLine("scope = '{0}',", this.Scope);
            textWriter.WriteLine("static = {0},", (this.IsStatic || this.IsConst).ToString().ToLower());
            textWriter.Indent--;
            textWriter.WriteLine("});");
        }

        public void WriteDefaultValue(IIndentedTextWriterWrapper textWriter, IProviders providers, bool @static = false)
        {
            if ((this.IsStatic || this.IsConst) != @static)
            {
                return;
            }

            this.variableVisitor.WriteDefaultValue(textWriter, providers);
        }

        public void WriteInitializeValue(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            this.variableVisitor.WriteInitializeValue(textWriter, providers);
        }
    }
}