namespace CsLuaConverter.CodeTreeLuaVisitor.Statement
{
    using System;
    using System.Linq;

    using CodeTree;
    using CsLuaConverter.Context;
    using CsLuaFramework;

    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    public class ThrowStatementVisitor : BaseVisitor
    {
        const string LuaHeader = "/* LUA";

        private readonly IVisitor inner;
        public ThrowStatementVisitor(CodeTreeBranch branch) : base(branch)
        {
            this.ExpectKind(0, SyntaxKind.ThrowKeyword);
            this.inner = this.CreateVisitor(1);
            this.ExpectKind(2, SyntaxKind.SemicolonToken);
        }

        public override void Visit(IIndentedTextWriterWrapper textWriter, IContext context)
        {
            var typeSymbol = context.SemanticModel.GetTypeInfo(this.Branch.SyntaxNode.ChildNodes().First()).Type;

            if (typeSymbol.Name == nameof(ReplaceWithLuaBlock))
            {
                this.WriteLuaCodeFromCommentBlock(textWriter);
                return;
            }

            textWriter.Write("_M.Throw(");
            this.inner.Visit(textWriter, context);
            textWriter.WriteLine(");");
        }

        private void WriteLuaCodeFromCommentBlock(IIndentedTextWriterWrapper textWriter)
        {
            var luaComments =
                this.Branch.SyntaxNode.Parent.DescendantTrivia()
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