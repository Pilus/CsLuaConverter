namespace CsLuaConverter.CodeTreeLuaVisitor.Expression
{
    using System;
    using CodeTree;
    using Microsoft.CodeAnalysis.CSharp;
    using Providers;
    using Providers.TypeKnowledgeRegistry;
    using Type;

    public class TypeOfExpressionVisitor : BaseVisitor
    {
        private readonly ITypeVisitor typeVisitor;
        public TypeOfExpressionVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.ExpectKind(0, SyntaxKind.TypeOfKeyword);
            this.ExpectKind(1, SyntaxKind.OpenParenToken);
            this.ExpectKind(3, SyntaxKind.CloseParenToken);
            this.typeVisitor = (ITypeVisitor) this.CreateVisitor(2);
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            this.typeVisitor.WriteAsType(textWriter, providers);
            providers.Context.CurrentType = new TypeKnowledge(typeof(Type));
        }
    }
}