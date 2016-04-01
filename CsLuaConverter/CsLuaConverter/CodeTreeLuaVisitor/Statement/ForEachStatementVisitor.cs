namespace CsLuaConverter.CodeTreeLuaVisitor.Statement
{
    using System;
    using System.CodeDom.Compiler;
    using CodeTree;
    using Microsoft.CodeAnalysis.CSharp;
    using Providers;
    using Providers.TypeProvider;
    using Type;

    public class ForEachStatementVisitor : BaseVisitor
    {
        private readonly ITypeVisitor iteratorType;
        private readonly string iteratorName;
        private readonly IVisitor enumerator;
        private readonly BlockVisitor block;

        public ForEachStatementVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.ExpectKind(0, SyntaxKind.ForEachKeyword);
            this.ExpectKind(1, SyntaxKind.OpenParenToken);
            this.iteratorType = (ITypeVisitor) this.CreateVisitor(2);
            this.ExpectKind(3, SyntaxKind.IdentifierToken);
            this.iteratorName = ((CodeTreeLeaf) this.Branch.Nodes[3]).Text;
            this.ExpectKind(4, SyntaxKind.InKeyword);
            this.enumerator = this.CreateVisitor(5);
            this.ExpectKind(6, SyntaxKind.CloseParenToken);
            this.ExpectKind(7, SyntaxKind.Block);
            this.block = (BlockVisitor)this.CreateVisitor(7);
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            var scope = providers.NameProvider.CloneScope();

            textWriter.Write("for _,{0} in (", this.iteratorName);
            this.enumerator.Visit(textWriter, providers);
            textWriter.WriteLine("%_M.DOT).GetEnumerator() do");

            var iteratorTypeKnowledge = this.iteratorType.GetType(providers);

            if (iteratorTypeKnowledge == null)
            {
                iteratorTypeKnowledge = providers.TypeKnowledgeRegistry.CurrentType.GetEnumeratorType();
            }

            providers.NameProvider.AddToScope(new ScopeElement(this.iteratorName, iteratorTypeKnowledge));

            
            this.block.Visit(textWriter, providers);
            textWriter.WriteLine("end");

            providers.NameProvider.SetScope(scope);
        }
    }
}