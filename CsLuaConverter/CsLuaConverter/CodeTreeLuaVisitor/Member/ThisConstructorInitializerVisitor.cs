namespace CsLuaConverter.CodeTreeLuaVisitor.Member
{
    using CodeTree;
    using Expression;
    using Lists;
    using Microsoft.CodeAnalysis.CSharp;
    using Providers;
    using Providers.TypeKnowledgeRegistry;

    public class ThisConstructorInitializerVisitor : BaseVisitor
    {
        private readonly ArgumentListVisitor argumentList;
        public ThisConstructorInitializerVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.ExpectKind(0, SyntaxKind.ColonToken);
            this.ExpectKind(1, SyntaxKind.ThisKeyword);
            this.ExpectKind(2, SyntaxKind.ArgumentList);
            this.argumentList = (ArgumentListVisitor)this.CreateVisitor(2);
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IProviders providers)
        {
            var thisType = providers.NameProvider.GetScopeElement("this");
            providers.Context.PossibleMethods = new PossibleMethods(thisType.Type.GetConstructors());

            var argumentWriter = textWriter.CreateTextWriterAtSameIndent();

            this.argumentList.Visit(argumentWriter, providers);

            var cstor = providers.Context.PossibleMethods.GetOnlyRemainingMethodOrThrow();

            textWriter.Write("(element % _M.DOT_LVL(typeObject.Level))");

            var signatureWriter = textWriter.CreateTextWriterAtSameIndent();
            var hasGenericComponents = cstor.WriteSignature(signatureWriter, providers);

            if (hasGenericComponents)
            {
                textWriter.Write("['_C_0_'..(");
                textWriter.AppendTextWriter(signatureWriter);
                textWriter.Write(")]");
            }
            else
            {
                textWriter.Write("._C_0_");
                textWriter.AppendTextWriter(signatureWriter);
            }

            textWriter.AppendTextWriter(argumentWriter);

            textWriter.WriteLine(";");
            providers.Context.CurrentType = null;
        }
    }
}