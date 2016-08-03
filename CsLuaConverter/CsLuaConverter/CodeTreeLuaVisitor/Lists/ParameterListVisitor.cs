namespace CsLuaConverter.CodeTreeLuaVisitor.Lists
{
    using System.Linq;
    using CodeTree;
    using Filters;
    using Microsoft.CodeAnalysis.CSharp;
    using Providers;
    using Providers.TypeKnowledgeRegistry;

    public class ParameterListVisitor : BaseVisitor
    {
        private readonly ParameterVisitor[] parameters;

        public string FirstElementPrefix = string.Empty;

        public ParameterListVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.parameters =
                this.CreateVisitors(new KindFilter(SyntaxKind.Parameter)).Select(v => (ParameterVisitor) v).ToArray();
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            if (this.parameters.Any())
            {
                textWriter.Write(this.FirstElementPrefix);
            }

            var expectedTypeGenerics = providers.Context.ExpectedType?.GetGenerics();
            for (var index = 0; index < this.parameters.Length; index++)
            {
                var visitor = this.parameters[index];

                if (expectedTypeGenerics != null)
                {
                    providers.Context.ExpectedType = expectedTypeGenerics[index];
                }

                visitor.Visit(textWriter, providers);

                if (index != this.parameters.Length - 1)
                {
                    textWriter.Write(", ");
                }
            }
        }

        public void WriteAsTypes(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            for (var index = 0; index < this.parameters.Length; index++)
            {
                var visitor = this.parameters[index];
                visitor.WriteAsTypes(textWriter, providers);

                if (index != this.parameters.Length - 1)
                {
                    textWriter.Write(", ");
                }
            }
        }

        public TypeKnowledge[] GetTypes(IProviders providers)
        {
            return this.parameters.Select(p => p.GetType(providers)).ToArray();
        }

        public string[] GetNames()
        {
            return this.parameters.Select(p => p.GetName()).ToArray();
        }

        public int GetNumParameters()
        {
            return this.parameters.Length;
        }
    }
}