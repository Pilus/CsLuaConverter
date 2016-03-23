namespace CsLuaConverter.CodeTreeLuaVisitor.Expression
{
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using CodeTree;
    using Microsoft.CodeAnalysis.CSharp;
    using Providers;
    using Type;

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

        public override void Visit(IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.Write(".__Initialize({");
            this.elementVisitors.VisitAll(textWriter, providers, () => textWriter.Write(", "));
            textWriter.Write("})");
            providers.TypeKnowledgeRegistry.CurrentType = providers.TypeKnowledgeRegistry.CurrentType.GetAsArrayType();
        }
    }
}