namespace CsLuaConverter.SyntaxExtensions
{
    using System.Linq;
    using CsLuaConverter.Context;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    public static class ListExtensions
    {
        public static void Write(this ArgumentListSyntax syntax, IIndentedTextWriterWrapper textWriter, IContext context)
        {
            textWriter.Write("(");
            syntax.Arguments.Write(SyntaxNodeExtensions.Write, textWriter, context);
            textWriter.Write(")");
        }

        public static void Write(this BracketedArgumentListSyntax syntax, IIndentedTextWriterWrapper textWriter, IContext context)
        {
            textWriter.Write("[");
            syntax.Arguments.Single().Write(textWriter, context);
            textWriter.Write("]");
        }

        public static void Write(ParameterListSyntax syntax, IIndentedTextWriterWrapper textWriter, IContext context)
        {
            if (syntax.Parameters.Any() && (syntax.Parent is ConstructorDeclarationSyntax || syntax.Parent is MethodDeclarationSyntax))
            {
                textWriter.Write(", ");
            }

            syntax.Parameters.Write(SyntaxNodeExtensions.Write, textWriter, context);
        }
    }
}