namespace CsLuaConverter.CodeTreeLuaVisitor.Lists
{
    using System;
    using System.CodeDom.Compiler;
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

        public override void Visit(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            if (this.parameters.Any())
            {
                textWriter.Write(this.FirstElementPrefix);
            }
            this.parameters.VisitAll(textWriter, providers, ", ");
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
    }
}