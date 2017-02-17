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
            syntax.Write(textWriter, context);
        }
    }
}