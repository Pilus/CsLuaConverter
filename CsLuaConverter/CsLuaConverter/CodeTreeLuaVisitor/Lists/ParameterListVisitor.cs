namespace CsLuaConverter.CodeTreeLuaVisitor.Lists
{
    using System.Linq;
    using CodeTree;
    using Filters;
    using Microsoft.CodeAnalysis.CSharp;
    using Providers;

    public class ParameterListVisitor : BaseVisitor
    {
        private readonly ParameterVisitor[] parameters;

        public string FirstElementPrefix = string.Empty;

        public ParameterListVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.parameters =
                this.CreateVisitors(new KindFilter(SyntaxKind.Parameter)).Select(v => (ParameterVisitor) v).ToArray();
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IContext context)
        {
            if (this.parameters.Any())
            {
                textWriter.Write(this.FirstElementPrefix);
            }

            for (var index = 0; index < this.parameters.Length; index++)
            {
                var visitor = this.parameters[index];

                visitor.Visit(textWriter, context);

                if (index != this.parameters.Length - 1)
                {
                    textWriter.Write(", ");
                }
            }
        }
    }
}