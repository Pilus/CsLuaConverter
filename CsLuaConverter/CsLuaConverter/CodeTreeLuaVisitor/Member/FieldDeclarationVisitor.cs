namespace CsLuaConverter.CodeTreeLuaVisitor.Member
{
    using System;
    using System.Linq;
    using CodeTree;
    using CsLuaConverter.Context;
    using Filters;
    using Microsoft.CodeAnalysis;
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
            this.Write(this.Branch.SyntaxNode as FieldDeclarationSyntax, textWriter, context);
        }

        public void Write(FieldDeclarationSyntax syntax, IIndentedTextWriterWrapper textWriter, IContext context)
        {
            var symbol = (IFieldSymbol)context.SemanticModel.GetDeclaredSymbol(syntax.Declaration.Variables.Single());
            
            textWriter.WriteLine("_M.IM(members, '{0}', {{", symbol.Name);
            textWriter.Indent++;
            textWriter.WriteLine("level = typeObject.Level,");
            textWriter.WriteLine("memberType = 'Field',");
            textWriter.WriteLine("scope = '{0}',", symbol.DeclaredAccessibility);
            textWriter.WriteLine("static = {0},", symbol.IsStatic.ToString().ToLower());
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

        public static void WriteInitializeValue(FieldDeclarationSyntax syntax, IIndentedTextWriterWrapper textWriter, IContext context)
        {
            var name = syntax.Declaration.Variables.Single().Identifier.Text;
            textWriter.WriteLine($"if not(values.{name} == nil) then element[typeObject.Level].{name} = values.{name}; end");
        }
    }
}