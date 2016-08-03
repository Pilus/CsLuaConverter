namespace CsLuaConverter.CodeTreeLuaVisitor.Expression
{
    using System.Collections.Generic;
    using System.Linq;
    using CodeTree;
    using Microsoft.CodeAnalysis.CSharp;
    using Providers;

    public class ArrayInitializerExpressionVisitor : BaseVisitor
    {
        private readonly IVisitor[] elementVisitors;

        public ArrayInitializerExpressionVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.ExpectKind(0, SyntaxKind.OpenBraceToken);
            var visitors = new List<IVisitor>();

            for (var i = 1; i < this.Branch.Nodes.Length - 1; i = i + 2)
            {
                visitors.Add(this.CreateVisitor(i));
                this.ExpectKind(i + 1, SyntaxKind.CommaToken, SyntaxKind.CloseBraceToken);
            }

            this.elementVisitors = visitors.ToArray();
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            textWriter.Write(".__Initialize({");

            if (this.elementVisitors.Any())
            {
                textWriter.Write("[0] = ");
            }

            var initializingType = providers.Context.CurrentType;

            providers.Context.CurrentType = null;
            this.elementVisitors.VisitAll(textWriter, providers, () =>
            {
                textWriter.Write(", ");
                providers.Context.CurrentType = null;
            });
            textWriter.Write("})");

            providers.Context.CurrentType = initializingType ?? providers.Context.CurrentType;

            if (providers.Context.CurrentType.IsArray())
            {
                return;
            }

            providers.Context.CurrentType = providers.Context.CurrentType.GetAsArrayType();
        }
    }
}