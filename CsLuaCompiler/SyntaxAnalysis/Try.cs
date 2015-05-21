
namespace CsLuaCompiler.SyntaxAnalysis
{
    using System;
    using System.CodeDom.Compiler;
    using CsLuaCompiler.Providers;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp.Syntax;
    using System.Collections.Generic;
    using CsLuaCompiler.Providers.TypeProvider;

    internal class Try : ILuaElement
    {
        private Block tryBlock;
        private IList<Catch> catches = new List<Catch>();
        private Block finallyBlock;

        public SyntaxToken Analyze(SyntaxToken token)
        {
            LuaElementHelper.CheckType(typeof(TryStatementSyntax), token.Parent);
            token = token.GetNextToken();

            this.tryBlock = new Block();
            token = this.tryBlock.Analyze(token);
            token = token.GetNextToken();

            while (token.Parent is CatchClauseSyntax)
            {
                var theCatch = new Catch();

                token = token.GetNextToken();
                if (token.Parent is CatchDeclarationSyntax)
                {
                    token = token.GetNextToken();
                    theCatch.Type = token.Text;
                    token = token.GetNextToken();
                    if (token.Parent is CatchDeclarationSyntax && token.Text != ")")
                    {
                        theCatch.VariableName = token.Text;
                        token = token.GetNextToken();
                    }
                    LuaElementHelper.CheckType(typeof(CatchDeclarationSyntax), token.Parent);
                    token = token.GetNextToken();
                }

                theCatch.Block = new Block();
                token = theCatch.Block.Analyze(token);
                this.catches.Add(theCatch);
                token = token.GetNextToken();
            }

            if (token.Parent is FinallyClauseSyntax)
            {
                token = token.GetNextToken();
                this.finallyBlock = new Block();
                token = this.finallyBlock.Analyze(token);
                token = token.GetNextToken();
            }

            return token.GetPreviousToken();
        }

        public void WriteLua(IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.WriteLine("__Try(function()");
            textWriter.Indent++;
            textWriter.Indent++;
            this.tryBlock.WriteLua(textWriter, providers);
            textWriter.Indent--;
            textWriter.WriteLine("end,");
            textWriter.WriteLine("{");
            textWriter.Indent++;
            foreach (var aCatch in this.catches)
            {
                textWriter.WriteLine("{");
                textWriter.Indent++;
                if (aCatch.Type != null)
                {
                    textWriter.WriteLine("type = '{0}',", providers.TypeProvider.LookupType(aCatch.Type));
                }
                
                textWriter.WriteLine("func = function({0})", aCatch.VariableName ?? "");

                var scope = providers.NameProvider.CloneScope();
                if (aCatch.VariableName != null)
                {
                    providers.NameProvider.AddToScope(new ScopeElement(aCatch.VariableName));
                }

                textWriter.Indent++;
                aCatch.Block.WriteLua(textWriter, providers);
                textWriter.Indent--;

                providers.NameProvider.SetScope(scope);

                textWriter.WriteLine("end,");
                textWriter.Indent--;
                textWriter.WriteLine("},");
            }
            textWriter.Indent--;
            textWriter.WriteLine("},");

            if (this.finallyBlock != null)
            {
                textWriter.WriteLine("function()");
                textWriter.Indent++;
                this.finallyBlock.WriteLua(textWriter, providers);
                textWriter.Indent--;
                textWriter.WriteLine("end");
            }
            else
            {
                textWriter.WriteLine("nil");
            }

            textWriter.Indent--;
            textWriter.WriteLine(");");            
        }
    }

    internal struct Catch
    {
        public string Type;
        public string VariableName;
        public Block Block;
    }
}
