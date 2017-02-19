namespace CsLuaConverter.CodeTreeLuaVisitor.Member
{
    using System;
    using System.Linq;

    using Attribute;
    using CodeTree;
    using Constraint;
    using CsLuaConverter.Context;
    using CsLuaConverter.SyntaxExtensions;
    using Filters;
    using Lists;

    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    public class MethodDeclarationVisitor : BaseVisitor
    {
        private readonly string name;
        private readonly TypeParameterListVisitor methodGenerics;
        private readonly Scope scope;
        private readonly bool isStatic;
        private readonly bool isOverride;
        private readonly ParameterListVisitor parameters;
        private readonly BlockVisitor block;
        private readonly AttributeListVisitor attributeList;
        private readonly TypeParameterConstraintClauseVisitor genericsConstraint;

        public MethodDeclarationVisitor(CodeTreeBranch branch) : base(branch)
        {
            var offset = 0;
            if (branch.Nodes[0].Kind == SyntaxKind.AttributeList)
            {
                this.attributeList = (AttributeListVisitor)this.CreateVisitor(0);
                offset++;
            }

            var accessorAndTypeFilter = new KindRangeFilter(branch.Nodes[offset].Kind, SyntaxKind.IdentifierToken);
            var accessorAndType = accessorAndTypeFilter.Filter(this.Branch.Nodes).ToArray();
            this.methodGenerics =
                (TypeParameterListVisitor) this.CreateVisitors(new KindFilter(SyntaxKind.TypeParameterList)).SingleOrDefault();
            this.name =
                ((CodeTreeLeaf) (new KindFilter(SyntaxKind.IdentifierToken)).Filter(this.Branch.Nodes).Single()).Text;

            if (accessorAndType.Length > 1 && ((CodeTreeLeaf)accessorAndType[0]).Text != "static")
            {
                this.scope = (Scope) Enum.Parse(typeof (Scope), ((CodeTreeLeaf) accessorAndType[0]).Text, true);
            }
            else
            {
                this.scope = Scope.Public;
            }

            this.isStatic = this.Branch.Nodes.OfType<CodeTreeLeaf>().Any(n => n.Kind.Equals(SyntaxKind.StaticKeyword));
            this.isOverride = this.Branch.Nodes.OfType<CodeTreeLeaf>().Any(n => n.Kind.Equals(SyntaxKind.OverrideKeyword));

            this.parameters = (ParameterListVisitor)this.CreateVisitors(new KindFilter(SyntaxKind.ParameterList)).Single();
            this.block = (BlockVisitor)this.CreateVisitors(new KindFilter(SyntaxKind.Block)).SingleOrDefault();
            this.genericsConstraint = (TypeParameterConstraintClauseVisitor)this.CreateVisitors(new KindFilter(SyntaxKind.TypeParameterConstraintClause)).SingleOrDefault();
        }
        

        public override void Visit(IIndentedTextWriterWrapper textWriter, IContext context)
        {
            var syntax = this.Branch.SyntaxNode as MethodDeclarationSyntax;
            var symbol = context.SemanticModel.GetDeclaredSymbol(syntax);

            WriteMethodGenericsMapping(syntax, textWriter, context);
            this.WriteMethodMember(syntax, textWriter, context, symbol);
        }

        public ITypeSymbol GetExtensionTypeSymbol(IContext context)
        {
            var symbol = context.SemanticModel.GetDeclaredSymbol(this.Branch.SyntaxNode as MethodDeclarationSyntax);
            if (!symbol.IsExtensionMethod)
            {
                return null;
            }



            return symbol.Parameters.Single(p => p.IsThis).Type;
        }

        public void WriteAsExtensionMethod(IIndentedTextWriterWrapper textWriter, IContext context)
        {
            var symbol = context.SemanticModel.GetDeclaredSymbol(this.Branch.SyntaxNode as MethodDeclarationSyntax);
            if (!symbol.IsExtensionMethod)
            {
                return;
            }


        }

        private void WriteMethodMember(MethodDeclarationSyntax syntax, IIndentedTextWriterWrapper textWriter, IContext context, IMethodSymbol symbol)
        {
            textWriter.WriteLine("_M.IM(members, '{0}', {{", this.name);
            textWriter.Indent++;

            WriteLevel(textWriter);
            WriteMemberType(textWriter);
            WriteScope(textWriter, symbol);
            WriteIsStatic(textWriter, symbol);
            WriteNumOfMethodGenerics(textWriter, symbol);
            WriteSignatureHash(textWriter, context, symbol);
            WriteOverride(textWriter, symbol);
            WriteIsParams(textWriter, symbol);
            WriteReturnType(textWriter, context, symbol);
            WriteGenerics(syntax, textWriter);
            this.WriteBodyFunc(syntax, textWriter, context, symbol);

            textWriter.Indent--;
            textWriter.WriteLine("});");
        }

        private void WriteBodyFunc(MethodDeclarationSyntax syntax, IIndentedTextWriterWrapper textWriter, IContext context, IMethodSymbol symbol)
        {
            if (syntax.Body == null)
            {
                return;
            }

            textWriter.Write("func = function(element");

            if (syntax.TypeParameterList != null)
            {
                textWriter.Write(", methodGenericsMapping, methodGenerics");
            }

            //this.parameters.FirstElementPrefix = ", ";
            //this.parameters.Visit(textWriter, context);
            syntax.ParameterList.Write(textWriter, context);

            textWriter.WriteLine(")");

            if (symbol.Parameters.LastOrDefault()?.IsParams == true)
            {
                textWriter.Indent++;
                WriteParamVariableInit(textWriter, context, symbol);
                textWriter.Indent--;
            }

            this.block.Visit(textWriter, context);
            textWriter.WriteLine("end");
        }

        private static void WriteGenerics(MethodDeclarationSyntax syntax, IIndentedTextWriterWrapper textWriter)
        {
            if (syntax.TypeParameterList != null)
            {
                textWriter.WriteLine("generics = methodGenericsMapping,");
            }
        }

        private static void WriteReturnType(IIndentedTextWriterWrapper textWriter, IContext context, IMethodSymbol symbol)
        {
            if (symbol.ReturnsVoid)
            {
                return;
            }

            textWriter.Write("returnType = function() return ");
            context.TypeReferenceWriter.WriteTypeReference(symbol.ReturnType, textWriter);
            textWriter.WriteLine(" end,");
        }

        private static void WriteIsParams(IIndentedTextWriterWrapper textWriter, IMethodSymbol symbol)
        {
            if (symbol.Parameters.LastOrDefault()?.IsParams == true)
            {
                textWriter.WriteLine("isParams = true,");
            }
        }

        private static void WriteOverride(IIndentedTextWriterWrapper textWriter, IMethodSymbol symbol)
        {
            if (symbol.IsOverride)
            {
                textWriter.WriteLine("override = true,");
            }
        }

        private static void WriteSignatureHash(
            IIndentedTextWriterWrapper textWriter,
            IContext context,
            IMethodSymbol symbol)
        {
            textWriter.Write("signatureHash = ");
            context.SignatureWriter.WriteSignature(symbol.Parameters.Select(p => p.Type).ToArray(), textWriter);
            textWriter.WriteLine(",");
        }

        private static void WriteNumOfMethodGenerics(IIndentedTextWriterWrapper textWriter, IMethodSymbol symbol)
        {
            textWriter.WriteLine("numMethodGenerics = {0},", symbol.TypeArguments.Length);
        }

        private static void WriteIsStatic(IIndentedTextWriterWrapper textWriter, IMethodSymbol symbol)
        {
            textWriter.WriteLine("static = {0},", symbol.IsStatic.ToString().ToLower());
        }

        private static void WriteScope(IIndentedTextWriterWrapper textWriter, IMethodSymbol symbol)
        {
            textWriter.WriteLine("scope = '{0}',", symbol.DeclaredAccessibility);
        }

        private static void WriteMemberType(IIndentedTextWriterWrapper textWriter)
        {
            textWriter.WriteLine("memberType = 'Method',");
        }

        private static void WriteLevel(IIndentedTextWriterWrapper textWriter)
        {
            textWriter.WriteLine("level = typeObject.Level,");
        }

        private static void WriteMethodGenericsMapping(MethodDeclarationSyntax syntax, IIndentedTextWriterWrapper textWriter, IContext context)
        {
            if (syntax.TypeParameterList == null)
            {
                return;
            }

            textWriter.Write("local methodGenericsMapping = {");
            syntax.TypeParameterList.Write(textWriter, context);
            textWriter.WriteLine("};");
            textWriter.WriteLine("local methodGenerics = _M.MG(methodGenericsMapping);");
        }

        private static void WriteParamVariableInit(IIndentedTextWriterWrapper textWriter, IContext context, IMethodSymbol symbol)
        {
            var parameter = symbol.Parameters.Last();
            textWriter.Write("local ");
            textWriter.Write(parameter.Name);
            textWriter.Write(" = (");
            context.TypeReferenceWriter.WriteInteractionElementReference(parameter.Type, textWriter);
            textWriter.WriteLine("._C_0_0() % _M.DOT).__Initialize({[0] = firstParam, ...});");
        }
    }
}