namespace CsLuaConverter.CodeTreeLuaVisitor.Expression
{
    using System;
    using System.CodeDom.Compiler;
    using System.Linq;
    using CodeTree;
    using Microsoft.CodeAnalysis.CSharp;
    using Providers;
    using Providers.TypeKnowledgeRegistry;

    public class NumericLiteralExpressionVisitor : BaseVisitor
    {
        private readonly string value;

        public NumericLiteralExpressionVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.ExpectKind(0, SyntaxKind.NumericLiteralToken);
            this.value = ((CodeTreeLeaf) this.Branch.Nodes.Single()).Text;
        }

        public override void Visit(IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.Write(this.value);
            providers.TypeKnowledgeRegistry.CurrentType = this.GetTypeKnowledge();
        }

        private TypeKnowledge GetTypeKnowledge()
        {
            return new TypeKnowledge(this.DetermineType());
        }

        private Type DetermineType()
        {
            return this.value.Contains(".") ? typeof (float) : typeof (int);
        }
    }
}