namespace CsLuaConverter.CodeTreeLuaVisitor.Member
{
    using CodeTree;
    using Expression;
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

            var argumentWriter = textWriter.CreateTextWriterAtSameIndent();

            this.argumentList.Visit(argumentWriter, providers);

            var cstor = providers.TypeKnowledgeRegistry.PossibleMethods.GetOnlyRemainingMethodOrThrow();

            textWriter.Write("(element % _M.DOT_LVL(typeObject.Level - 1))._C_0_");

            cstor.WriteSignature(textWriter, providers);

            textWriter.AppendTextWriter(argumentWriter);

            textWriter.WriteLine(";");
            providers.TypeKnowledgeRegistry.CurrentType = null;
        }

        
    }
}