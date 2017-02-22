namespace CsLuaSyntaxTranslator.SyntaxExtensions
{
    using System;
    using System.Linq;
    using CsLuaSyntaxTranslator.Context;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;
    using CSharpExtensions = Microsoft.CodeAnalysis.CSharpExtensions;
    using CsLuaFramework;

    public static class TryCatchExtensions
    {
        public static void Write(this TryStatementSyntax syntax, IIndentedTextWriterWrapper textWriter, IContext context)
        {
            textWriter.WriteLine("_M.Try(");
            textWriter.Indent++;

            textWriter.WriteLine("function()");
            syntax.Block.Write(textWriter, context);
            textWriter.WriteLine("end,");
            textWriter.WriteLine("{");
            textWriter.Indent++;

            syntax.Catches.Write(Write, textWriter, context);

            textWriter.Indent--;
            textWriter.WriteLine("},");

            if (syntax.Finally != null)
            {
                textWriter.WriteLine("function()");
                syntax.Finally.Write(textWriter, context);
                textWriter.WriteLine("end");
            }
            else
            {
                textWriter.WriteLine("nil");
            }

            textWriter.Indent--;
            textWriter.WriteLine(");");
        }

        public static void Write(this CatchClauseSyntax syntax, IIndentedTextWriterWrapper textWriter, IContext context)
        {
            textWriter.WriteLine("{");
            textWriter.Indent++;

            if (syntax.Declaration != null)
            {
                syntax.Declaration.Write(textWriter, context);
            }
            else
            {
                textWriter.WriteLine("func = function()");
            }

            syntax.Block.Write(textWriter, context);

            textWriter.WriteLine("end,");

            textWriter.Indent--;
            textWriter.WriteLine("},");
        }

        public static void Write(this CatchDeclarationSyntax syntax, IIndentedTextWriterWrapper textWriter, IContext context)
        {
            if (syntax.Type != null)
            {
                var symbol = (ITypeSymbol)context.SemanticModel.GetSymbolInfo(syntax.Type).Symbol;
                textWriter.Write("type = ");
                context.TypeReferenceWriter.WriteTypeReference(symbol, textWriter);
                textWriter.WriteLine(",");
            }

            textWriter.WriteLine("func = function({0})", syntax.Identifier.Text);
        }

        public static void Write(this FinallyClauseSyntax syntax, IIndentedTextWriterWrapper textWriter, IContext context)
        {
            syntax.Block.Write(textWriter, context);
        }

        private const string LuaHeader = "/* LUA";

        public static void Write(ThrowStatementSyntax syntax, IIndentedTextWriterWrapper textWriter, IContext context)
        {
            var typeSymbol = context.SemanticModel.GetTypeInfo(syntax.Expression).Type;

            if (typeSymbol.Name == nameof(ReplaceWithLuaBlock))
            {
                WriteLuaCodeFromCommentBlock(syntax, textWriter);
                return;
            }

            textWriter.Write("_M.Throw(");
            syntax.Expression.Write(textWriter, context);
            textWriter.WriteLine(");");
        }

        private static void WriteLuaCodeFromCommentBlock(ThrowStatementSyntax syntax, IIndentedTextWriterWrapper textWriter)
        {
            var luaComments =
                syntax.Expression.Parent.DescendantTrivia()
                    .Where(t => CSharpExtensions.IsKind((SyntaxTrivia) t, SyntaxKind.MultiLineCommentTrivia) && t.ToString().StartsWith(LuaHeader))
                    .ToArray();

            if (luaComments.Length == 0)
            {
                throw new Exception(
                    $"{nameof(ReplaceWithLuaBlock)} exception was thrown, but could not find any block starting with '{LuaHeader}'.");
            }

            if (luaComments.Length > 1)
            {
                throw new Exception($"Multiple ({luaComments.Length}) lua blocks found in same scope.");
            }

            textWriter.WriteLine(luaComments.Single().ToFullString().Replace(LuaHeader, "").TrimEnd('/').TrimEnd('*'));
        }
    }
}