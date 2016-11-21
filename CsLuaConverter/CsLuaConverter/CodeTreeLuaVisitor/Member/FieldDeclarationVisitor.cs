namespace CsLuaConverter.CodeTreeLuaVisitor.Member
{
    using System;
    using System.Linq;
    using CodeTree;
    using CsLuaConverter.Context;
    using Filters;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    public class FieldDeclarationVisitor : BaseVisitor
    {

        private readonly VariableDeclarationVisitor variableVisitor;

        public bool IsStatic { get; }

        public bool IsConst { get; }

        public Scope Scope { get; }

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

        public override void Visit(IIndentedTextWriterWrapper textWriter, IContext context)
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

        public static void WriteDefaultValue(FieldDeclarationSyntax syntax, IIndentedTextWriterWrapper textWriter, IContext context, bool @static = false)
        {
            var isStatic = syntax.Modifiers.Any(n => n.GetKind().Equals(SyntaxKind.StaticKeyword));
            var isConst = syntax.Modifiers.Any(n => n.GetKind().Equals(SyntaxKind.ConstKeyword));

            if ((isStatic || isConst) != @static)
            {
                return;
            }

            VariableDeclarationVisitor.WriteDefaultValue(syntax.Declaration, textWriter, context);
        }

        public void WriteInitializeValue(IIndentedTextWriterWrapper textWriter, IContext context)
        {
            this.variableVisitor.WriteInitializeValue(textWriter, context);
        }
    }
}