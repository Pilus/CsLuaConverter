namespace CsLuaConverter.CodeTreeLuaVisitor.Statement
{
    using System;
    using System.Linq;

    using CodeTree;
    using CsLuaConverter.Context;
    using CsLuaConverter.SyntaxExtensions;
    using CsLuaFramework;

    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    public class ThrowStatementVisitor : BaseVisitor
    {
        const string LuaHeader = "/* LUA";

        public ThrowStatementVisitor(CodeTreeBranch branch) : base(branch)
        {

        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IContext context)
        {
            var syntax = (ThrowStatementSyntax)this.Branch.SyntaxNode;

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
                    .Where(t => t.IsKind(SyntaxKind.MultiLineCommentTrivia) && t.ToString().StartsWith(LuaHeader))
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