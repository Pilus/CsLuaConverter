namespace CsLuaConverter.CodeTreeLuaVisitor.Member
{
    using System;
    using System.Linq;
    using CodeTree;
    using Filters;
    using Lists;
    using Microsoft.CodeAnalysis.CSharp;
    using Providers;
    using Providers.GenericsRegistry;
    using Type;

    public class MethodDeclarationVisitor : BaseVisitor
    {
        private readonly string name;
        private readonly ITypeVisitor type;
        private readonly TypeParameterListVisitor methodGenerics;
        private readonly Scope scope;
        private readonly bool isStatic;
        private readonly bool isOverride;
        private readonly ParameterListVisitor parameters;
        private readonly BlockVisitor block;

        public MethodDeclarationVisitor(CodeTreeBranch branch) : base(branch)
        {
            var accessorAndTypeFilter = new KindRangeFilter(null, SyntaxKind.IdentifierToken);
            var accessorAndType = accessorAndTypeFilter.Filter(this.Branch.Nodes).ToArray();
            this.type = (ITypeVisitor) this.CreateVisitor(accessorAndType.Length - 1);
            this.methodGenerics =
                (TypeParameterListVisitor) this.CreateVisitors(new KindFilter(SyntaxKind.TypeParameterList)).SingleOrDefault();
            this.name =
                ((CodeTreeLeaf) (new KindFilter(SyntaxKind.IdentifierToken)).Filter(this.Branch.Nodes).Single()).Text;

            if (accessorAndType.Length > 1)
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
            this.block = (BlockVisitor)this.CreateVisitors(new KindFilter(SyntaxKind.Block)).Single();
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            this.RegisterMethodGenerics(providers);

            if (this.methodGenerics != null)
            {
                textWriter.Write("local methodGenericsMapping = {");
                this.methodGenerics.Visit(textWriter, providers);
                textWriter.WriteLine("};");
                textWriter.WriteLine("local methodGenerics = _M.MG(methodGenericsMapping);");

                providers.GenericsRegistry.SetGenerics(this.methodGenerics.GetNames(), GenericScope.Method);
            }

            textWriter.WriteLine("_M.IM(members, '{0}', {{", this.name);
            textWriter.Indent++;

            textWriter.WriteLine("level = typeObject.Level,");
            textWriter.WriteLine("memberType = 'Method',");
            textWriter.WriteLine("scope = '{0}',", this.scope);
            textWriter.WriteLine("static = {0},", this.isStatic.ToString().ToLower());

            if (this.isOverride)
            {
                textWriter.WriteLine("override = true,");
            }

            if (this.parameters.GetTypes(providers).LastOrDefault()?.IsParams == true)
            {
                textWriter.WriteLine("isParams = true,");
            }

            textWriter.Write("types = {");
            this.parameters.WriteAsTypes(textWriter, providers);
            textWriter.WriteLine("},");

            if (this.methodGenerics != null)
            {
                textWriter.WriteLine("generics = methodGenericsMapping,");
            }

            textWriter.Write("func = function(element");

            if (this.methodGenerics != null)
            {
                textWriter.Write(", methodGenericsMapping, methodGenerics");
            }

            var scope = providers.NameProvider.CloneScope();

            this.parameters.FirstElementPrefix = ", ";
            this.parameters.Visit(textWriter, providers);

            textWriter.WriteLine(")");

            if (this.parameters.GetTypes(providers).LastOrDefault()?.IsParams == true)
            {
                textWriter.Indent++;
                this.WriteParamVariableInit(textWriter, providers);
                textWriter.Indent--;
            }

            this.block.Visit(textWriter, providers);
            textWriter.WriteLine("end");

            providers.NameProvider.SetScope(scope);

            textWriter.Indent--;
            textWriter.WriteLine("});");
        }

        public void WriteParamVariableInit(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            var names = this.parameters.GetNames();
            var types = this.parameters.GetTypes(providers);
            textWriter.Write("local ");
            textWriter.Write(names.Last());
            textWriter.Write(" = ((System.Array %_M.DOT)[{");
            types.Last().WriteAsType(textWriter, providers);
            textWriter.WriteLine("}]() % _M.DOT).__Initialize({[0] = firstParam, ...});");
        }

        private void RegisterMethodGenerics(IProviders providers)
        {
            if (this.methodGenerics == null)
            {
                providers.GenericsRegistry.SetGenerics(new string[] { }, GenericScope.Method);
                return;
            }
            
            // TODO: Register method generics
            //providers.GenericsRegistry.SetGenerics(element.MethodGenerics.ContainedElements.OfType<TypeParameter>().Select(e => e.Name), GenericScope.Method);
        }
    }
}