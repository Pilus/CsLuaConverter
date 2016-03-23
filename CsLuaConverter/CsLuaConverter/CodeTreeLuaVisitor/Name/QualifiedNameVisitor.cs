namespace CsLuaConverter.CodeTreeLuaVisitor.Name
{
    using System.CodeDom.Compiler;
    using System.Linq;
    using CodeTree;
    using Filters;
    using Microsoft.CodeAnalysis.CSharp;
    using Providers;
    using Type;

    public class QualifiedNameVisitor : BaseTypeVisitor, INameVisitor
    {
        private readonly INameVisitor[] visitors;

        public QualifiedNameVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.visitors = this.CreateVisitors(new KindFilter(SyntaxKind.IdentifierName, SyntaxKind.QualifiedName))
                .OfType<INameVisitor>().ToArray();
        }

        public override void Visit(IndentedTextWriter textWriter, IProviders providers)
        {
            throw new System.NotImplementedException();
        }

        public string[] GetName()
        {
            return this.visitors.SelectMany(v => v.GetName()).ToArray();
        }

        public override void WriteAsReference(IndentedTextWriter textWriter, IProviders providers)
        {
            var name = this.GetName();
            var type = providers.TypeProvider.LookupType(name);
            textWriter.Write(type.FullNameWithoutGenerics);

            var last = this.visitors.Last() as GenericNameVisitor;
            last?.WriteGenericTypes(textWriter, providers);
        }
    }
}