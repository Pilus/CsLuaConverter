namespace CsLuaConverter.CodeTreeLuaVisitor.Expression
{
    using System.Linq;
    using CodeTree;
    using CsLuaConverter.Context;
    using CsLuaConverter.SyntaxExtensions;

    using Filters;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    public class ImplicitArrayCreationExpressionVisitor : BaseVisitor
    {
        public ImplicitArrayCreationExpressionVisitor(CodeTreeBranch branch) : base(branch)
        {
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IContext context)
        {
            var syntax = (ImplicitArrayCreationExpressionSyntax)this.Branch.SyntaxNode;
            var typeInfo = context.SemanticModel.GetTypeInfo(syntax);
            textWriter.Write("(");
            context.TypeReferenceWriter.WriteInteractionElementReference(typeInfo.Type, textWriter);
            textWriter.Write("._C_0_0() % _M.DOT)");
            syntax.Initializer.Write(textWriter, context);
        }
    }
}