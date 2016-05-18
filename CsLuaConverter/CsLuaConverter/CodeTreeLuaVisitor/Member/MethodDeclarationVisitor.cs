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
            this.block = (BlockVisitor)this.CreateVisitors(new KindFilter(SyntaxKind.Block)).SingleOrDefault();
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            if (this.methodGenerics != null)
            {
                textWriter.Write("local methodGenericsMapping = {");
                this.methodGenerics.Visit(textWriter, providers);
                textWriter.WriteLine("};");
                textWriter.WriteLine("local methodGenerics = _M.MG(methodGenericsMapping);");

                foreach (var genericName in this.methodGenerics.GetNames())
                {
                    // TODO: Determine the correct object type for the generic.
                    providers.GenericsRegistry.SetGenerics(genericName, GenericScope.Method, typeof(object));
                }
            }

            textWriter.WriteLine("_M.IM(members, '{0}', {{", this.name);
            textWriter.Indent++;

            textWriter.WriteLine("level = typeObject.Level,");
            textWriter.WriteLine("memberType = 'Method',");
            textWriter.WriteLine("scope = '{0}',", this.scope);
            textWriter.WriteLine("static = {0},", this.isStatic.ToString().ToLower());
            textWriter.WriteLine("numMethodGenerics = {0},", this.methodGenerics?.GetNumElements() ?? 0);

            if (this.isOverride)
            {
                textWriter.WriteLine("override = true,");
            }

            if (this.parameters.GetTypes(providers).LastOrDefault()?.IsParams == true)
            {
                textWriter.WriteLine("isParams = true,");
            }

            if (this.type != null && (this.type as PredefinedTypeVisitor)?.IsVoid() != true)
            {
                textWriter.Write("returnType = ");
                this.type.WriteAsType(textWriter, providers);
                textWriter.WriteLine(",");
            }

            textWriter.Write("types = {");
            this.parameters.WriteAsTypes(textWriter, providers);
            textWriter.WriteLine("},");

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
            }

            providers.GenericsRegistry.ClearScope(GenericScope.Method);

            textWriter.Indent--;
            textWriter.WriteLine("});");
        }

        public void WriteParamVariableInit(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            var names = this.parameters.GetNames();
            var types = this.parameters.GetTypes(providers);
            textWriter.Write("local ");
            textWriter.Write(names.Last());
            textWriter.Write(" = ((");
            types.Last().WriteAsReference(textWriter, providers);
            textWriter.WriteLine(" % _M.DOT)() % _M.DOT).__Initialize({[0] = firstParam, ...});");
        }
    }
}