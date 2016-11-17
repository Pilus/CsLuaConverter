namespace CsLuaConverter.CodeTreeLuaVisitor.Member
{
    using System.Linq;
    using CodeTree;
    using Expression;
    using Lists;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Providers;

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
            var symbol = (IMethodSymbol)providers.SemanticModel.GetSymbolInfo(this.Branch.SyntaxNode).Symbol;
            
            textWriter.Write("(element % _M.DOT_LVL(typeObject.Level))");

            var signatureWriter = textWriter.CreateTextWriterAtSameIndent();
            var hasGenericComponents = providers.SignatureWriter.WriteSignature(symbol.Parameters.Select(p => p.Type).ToArray(), signatureWriter);

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

            this.argumentList.Visit(textWriter, providers);

            textWriter.WriteLine(";");
        }
    }
}