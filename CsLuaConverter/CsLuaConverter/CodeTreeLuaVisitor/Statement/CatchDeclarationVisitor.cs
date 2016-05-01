namespace CsLuaConverter.CodeTreeLuaVisitor.Statement
{
    using System.Linq;
    using CodeTree;
    using Filters;
    using Microsoft.CodeAnalysis.CSharp;
    using Providers;
    using Providers.TypeProvider;
    using Type;

    public class CatchDeclarationVisitor : BaseVisitor
    {
        private readonly ITypeVisitor type;
        private readonly string variableName;
        public CatchDeclarationVisitor(CodeTreeBranch branch) : base(branch)
        {
            var visitors =
                this.CreateVisitors(new KindRangeFilter(SyntaxKind.OpenParenToken, SyntaxKind.CloseParenToken));
            this.type = (ITypeVisitor) visitors.SingleOrDefault();
            this.variableName =
                branch.Nodes
                    .OfType<CodeTreeLeaf>()
                    .SingleOrDefault(l => l.Kind == SyntaxKind.IdentifierToken)?
                    .Text;
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            if (this.type != null)
            {
                textWriter.Write("type = ");
                this.type.WriteAsType(textWriter, providers);
                textWriter.WriteLine(",");
            }

            if (this.variableName != null)
            {
                providers.NameProvider.AddToScope(new ScopeElement(this.variableName, this.type.GetType(providers)));
            }

            textWriter.WriteLine("func = function({0})", this.variableName ?? string.Empty);
        }
    }
}