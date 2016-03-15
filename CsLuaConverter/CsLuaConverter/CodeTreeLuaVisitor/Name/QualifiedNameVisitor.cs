namespace CsLuaConverter.CodeTreeLuaVisitor.Name
{
    using System.CodeDom.Compiler;
    using System.Linq;
    using CodeTree;
    using Filters;
    using Microsoft.CodeAnalysis.CSharp;
    using Providers;

    public class QualifiedNameVisitor : BaseVisitor, INameVisitor
    {
        public QualifiedNameVisitor(CodeTreeBranch branch) : base(branch)
        {
        }

        public override void Visit(IndentedTextWriter textWriter, IProviders providers)
        {
            throw new System.NotImplementedException();
        }

        public string[] GetName()
        {
            return
                this.CreateVisitors(new KindFilter(SyntaxKind.IdentifierName, SyntaxKind.QualifiedName))
                    .OfType<INameVisitor>()
                    .SelectMany(v => v.GetName())
                    .ToArray();
        }
    }
}