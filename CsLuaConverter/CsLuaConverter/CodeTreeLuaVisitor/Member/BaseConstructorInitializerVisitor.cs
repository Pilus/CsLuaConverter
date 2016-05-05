namespace CsLuaConverter.CodeTreeLuaVisitor.Member
{
    using CodeTree;
    using Lists;
    using Microsoft.CodeAnalysis.CSharp;
    using Providers;
    using Providers.TypeKnowledgeRegistry;

    public class BaseConstructorInitializerVisitor : BaseVisitor
    {
        private readonly ArgumentListVisitor argumentList;
        public BaseConstructorInitializerVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.ExpectKind(0, SyntaxKind.ColonToken);
            this.ExpectKind(1, SyntaxKind.BaseKeyword);
            this.ExpectKind(2, SyntaxKind.ArgumentList);
            this.argumentList = (ArgumentListVisitor) this.CreateVisitor(2);
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            var baseType = providers.NameProvider.GetScopeElement("base");
            providers.TypeKnowledgeRegistry.PossibleMethods = new PossibleMethods(baseType.Type.GetConstructors());

            textWriter.Write("_M.BC(element, baseConstructors, ");

            this.argumentList.SkipOpeningParen = true;
            this.argumentList.Visit(textWriter, providers);

            textWriter.WriteLine(";");
            providers.TypeKnowledgeRegistry.CurrentType = null;
        }

        
    }
}