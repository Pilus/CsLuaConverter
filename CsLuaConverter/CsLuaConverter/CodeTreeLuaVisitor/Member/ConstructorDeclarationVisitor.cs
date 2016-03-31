namespace CsLuaConverter.CodeTreeLuaVisitor.Member
{
    using System.Linq;
    using CodeTree;
    using Filters;
    using Lists;
    using Microsoft.CodeAnalysis.CSharp;
    using Providers;

    public class ConstructorDeclarationVisitor : BaseVisitor
    {
        private ParameterListVisitor parameterList;
        private BaseListVisitor baseListVisitor;
        private BlockVisitor block;

        public ConstructorDeclarationVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.parameterList =
                (ParameterListVisitor) this.CreateVisitors(new KindFilter(SyntaxKind.ParameterList)).Single();
            this.block =
                (BlockVisitor)this.CreateVisitors(new KindFilter(SyntaxKind.Block)).Single();
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            textWriter.WriteLine("{");
            textWriter.Indent++;

            var scope = providers.NameProvider.CloneScope();

            textWriter.Write("types = {");
            this.parameterList.WriteAsTypes(textWriter, providers);
            textWriter.WriteLine("},");

            textWriter.Write("func = function(element");
            this.parameterList.FirstElementPrefix = ", ";
            this.parameterList.Visit(textWriter, providers);
            textWriter.WriteLine(")");

            textWriter.Indent++;
            if (this.baseListVisitor != null)
            {
                textWriter.Write("_M.BC(element, baseConstructors, ");
                //ArgumentListVisitor.WriteInner(constructor.BaseConstructorInitializer.ArgumentList, textWriter, providers);
                textWriter.WriteLine(");");
            }
            else
            {
                textWriter.WriteLine("_M.BC(element, baseConstructors);");
            }

            textWriter.Indent--;

            this.block.Visit(textWriter, providers);
            textWriter.WriteLine("end,");

            providers.NameProvider.SetScope(scope);

            textWriter.Indent--;
            textWriter.WriteLine("},");
        }
    }
}