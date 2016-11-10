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
        private readonly ITypeVisitor returnTypeVisitor;
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
            this.returnTypeVisitor = (ITypeVisitor) this.CreateVisitor(offset + accessorAndType.Length - 1);
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
        
        private static MethodBase[] GetAllMembers(Type type, string name)
        {
            var all = new List<MemberInfo>();
            all.AddRange(type.GetMembers(BindingFlags.Public | BindingFlags.Instance));
            all.AddRange(type.GetMembers(BindingFlags.NonPublic | BindingFlags.Instance));

            all.AddRange(type.GetMembers(BindingFlags.Public | BindingFlags.Static));
            all.AddRange(type.GetMembers(BindingFlags.NonPublic | BindingFlags.Static));

            return all.OfType<MethodBase>().Where(m => m.Name == name).ToArray();
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            var symbol = providers.SemanticModel.GetDeclaredSymbol(this.Branch.SyntaxNode as MethodDeclarationSyntax);
            //var definingType = providers.NameProvider.GetScopeElement("this").Type.GetTypeObject();
            //var genericObjects = GetAllMembers(definingType, this.name).SelectMany(m => m.GetGenericArguments()).ToList();

            if (this.methodGenerics != null)
            {
                textWriter.Write("local methodGenericsMapping = {");
                this.methodGenerics.Visit(textWriter, providers);
                textWriter.WriteLine("};");
                textWriter.WriteLine("local methodGenerics = _M.MG(methodGenericsMapping);");
            }

            textWriter.WriteLine("_M.IM(members, '{0}', {{", this.name);
            textWriter.Indent++;

            textWriter.WriteLine("level = typeObject.Level,");
            textWriter.WriteLine("memberType = 'Method',");
            textWriter.WriteLine("scope = '{0}',", this.scope);
            textWriter.WriteLine("static = {0},", symbol.IsStatic.ToString().ToLower());
            textWriter.WriteLine("numMethodGenerics = {0},", symbol.TypeArguments.Length);
            textWriter.Write("signatureHash = ");
            providers.SignatureWriter.WriteSignature(symbol.Parameters.Select(p => p.Type).ToArray(), textWriter);
            textWriter.WriteLine(",");


            if (this.isOverride)
            {
                textWriter.WriteLine("override = true,");
            }

            if (symbol.Parameters.LastOrDefault()?.IsParams == true)
            {
                textWriter.WriteLine("isParams = true,");
            }

            
            if (!symbol.ReturnsVoid)
            {
                textWriter.Write("returnType = ");
                providers.TypeReferenceWriter.WriteTypeReference(symbol.ReturnType, textWriter);
                textWriter.WriteLine(",");
            }
            /*
            textWriter.Write("types = {");
            this.parameters.WriteAsTypes(textWriter, providers);
            textWriter.WriteLine("},"); */

            if (this.methodGenerics != null)
            {
                textWriter.WriteLine("generics = methodGenericsMapping,");
            }

            if (this.block != null)
            { 
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

            textWriter.Indent--;
            textWriter.WriteLine("});");
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