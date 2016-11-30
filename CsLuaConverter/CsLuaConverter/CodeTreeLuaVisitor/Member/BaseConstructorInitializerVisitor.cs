namespace CsLuaConverter.CodeTreeLuaVisitor.Member
{
    using System.Linq;

    using CodeTree;
    using CsLuaConverter.Context;
    using CsLuaConverter.SyntaxExtensions;

    using Lists;

    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    public class BaseConstructorInitializerVisitor : BaseVisitor
    {
        public BaseConstructorInitializerVisitor(CodeTreeBranch branch) : base(branch)
        {
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IContext context)
        {
            var syntax = (ConstructorInitializerSyntax)this.Branch.SyntaxNode;
            Write(syntax, textWriter, context);
        }

        private static void Write(ConstructorInitializerSyntax syntax, IIndentedTextWriterWrapper textWriter, IContext context)
        {
            var symbol = (IMethodSymbol)context.SemanticModel.GetSymbolInfo(syntax).Symbol;
            textWriter.Write("(element % _M.DOT_LVL(typeObject.Level - 1))._C_0_");
            context.SignatureWriter.WriteSignature(symbol.Parameters.Select(p => p.Type).ToArray(), textWriter);
            syntax.ArgumentList.Write(textWriter, context);
            textWriter.WriteLine(";");
        }
    }
}