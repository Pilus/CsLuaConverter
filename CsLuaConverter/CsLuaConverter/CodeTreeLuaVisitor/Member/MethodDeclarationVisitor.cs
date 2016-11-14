namespace CsLuaConverter.CodeTreeLuaVisitor.Member
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Attribute;
    using CodeTree;
    using Constraint;
    using Filters;
    using Lists;

    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;
    using Providers;
    using Type;

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
        

        public override void Visit(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            var symbol = providers.SemanticModel.GetDeclaredSymbol(this.Branch.SyntaxNode as MethodDeclarationSyntax);
            /*if (symbol.IsExtensionMethod)
            {
                return;
            }*/

            this.WriteMethodGenericsMapping(textWriter, providers);
            this.WriteMethodMember(textWriter, providers, symbol);
        }

        public ITypeSymbol GetExtensionTypeSymbol(IProviders providers)
        {
            var symbol = providers.SemanticModel.GetDeclaredSymbol(this.Branch.SyntaxNode as MethodDeclarationSyntax);
            if (!symbol.IsExtensionMethod)
            {
                return null;
            }



            return symbol.Parameters.Single(p => p.IsThis).Type;
        }

        public void WriteAsExtensionMethod(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            var symbol = providers.SemanticModel.GetDeclaredSymbol(this.Branch.SyntaxNode as MethodDeclarationSyntax);
            if (!symbol.IsExtensionMethod)
            {
                return;
            }


        }

        private void WriteMethodMember(IIndentedTextWriterWrapper textWriter, IProviders providers, IMethodSymbol symbol)
        {
            textWriter.WriteLine("_M.IM(members, '{0}', {{", this.name);
            textWriter.Indent++;

            WriteLevel(textWriter);
            WriteMemberType(textWriter);
            this.WriteScope(textWriter);
            WriteIsStatic(textWriter, symbol);
            WriteNumOfMethodGenerics(textWriter, symbol);
            WriteSignatureHash(textWriter, providers, symbol);
            this.WriteOverride(textWriter);
            WriteIsParams(textWriter, symbol);
            WriteReturnType(textWriter, providers, symbol);
            this.WriteGenerics(textWriter);
            this.WriteBodyFunc(textWriter, providers, symbol);

            textWriter.Indent--;
            textWriter.WriteLine("});");
        }

        private void WriteBodyFunc(IIndentedTextWriterWrapper textWriter, IProviders providers, IMethodSymbol symbol)
        {
            if (this.block == null)
            {
                return;
            }

            textWriter.Write("func = function(element");

            if (this.methodGenerics != null)
            {
                textWriter.Write(", methodGenericsMapping, methodGenerics");
            }

            this.parameters.FirstElementPrefix = ", ";
            this.parameters.Visit(textWriter, providers);

            textWriter.WriteLine(")");

            if (symbol.Parameters.LastOrDefault()?.IsParams == true)
            {
                textWriter.Indent++;
                this.WriteParamVariableInit(textWriter, providers, symbol);
                textWriter.Indent--;
            }

            this.block.Visit(textWriter, providers);
            textWriter.WriteLine("end");
        }

        private void WriteGenerics(IIndentedTextWriterWrapper textWriter)
        {
            if (this.methodGenerics != null)
            {
                textWriter.WriteLine("generics = methodGenericsMapping,");
            }
        }

        private static void WriteReturnType(IIndentedTextWriterWrapper textWriter, IProviders providers, IMethodSymbol symbol)
        {
            if (symbol.ReturnsVoid)
            {
                return;
            }

            textWriter.Write("returnType = function() return ");
            providers.TypeReferenceWriter.WriteTypeReference(symbol.ReturnType, textWriter);
            textWriter.WriteLine(" end,");
        }

        private static void WriteIsParams(IIndentedTextWriterWrapper textWriter, IMethodSymbol symbol)
        {
            if (symbol.Parameters.LastOrDefault()?.IsParams == true)
            {
                textWriter.WriteLine("isParams = true,");
            }
        }

        private void WriteOverride(IIndentedTextWriterWrapper textWriter)
        {
            if (this.isOverride)
            {
                textWriter.WriteLine("override = true,");
            }
        }

        private static void WriteSignatureHash(
            IIndentedTextWriterWrapper textWriter,
            IProviders providers,
            IMethodSymbol symbol)
        {
            textWriter.Write("signatureHash = ");
            providers.SignatureWriter.WriteSignature(symbol.Parameters.Select(p => p.Type).ToArray(), textWriter);
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

        private void WriteScope(IIndentedTextWriterWrapper textWriter)
        {
            textWriter.WriteLine("scope = '{0}',", this.scope);
        }

        private static void WriteMemberType(IIndentedTextWriterWrapper textWriter)
        {
            textWriter.WriteLine("memberType = 'Method',");
        }

        private static void WriteLevel(IIndentedTextWriterWrapper textWriter)
        {
            textWriter.WriteLine("level = typeObject.Level,");
        }

        private void WriteMethodGenericsMapping(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            if (this.methodGenerics == null)
            {
                return;
            }

            textWriter.Write("local methodGenericsMapping = {");
            this.methodGenerics.Visit(textWriter, providers);
            textWriter.WriteLine("};");
            textWriter.WriteLine("local methodGenerics = _M.MG(methodGenericsMapping);");
        }

        private void WriteParamVariableInit(IIndentedTextWriterWrapper textWriter, IProviders providers, IMethodSymbol symbol)
        {
            var parameter = symbol.Parameters.Last();
            textWriter.Write("local ");
            textWriter.Write(parameter.Name);
            textWriter.Write(" = (");
            providers.TypeReferenceWriter.WriteInteractionElementReference(parameter.Type, textWriter);
            textWriter.WriteLine("._C_0_0() % _M.DOT).__Initialize({[0] = firstParam, ...});");
        }
    }
}