namespace CsLuaConverter.SyntaxAnalysis
{
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using CsLuaConverter.Providers;
    using CsLuaConverter.Providers.TypeProvider;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    internal class LambdaExpression : ILuaElement
    {
        private string argName;
        private ILuaElement content;

        public void WriteLua(IndentedTextWriter textWriter, IProviders providers)
        {
            List<ScopeElement> originalScope = providers.NameProvider.CloneScope();
            providers.NameProvider.AddToScope(new ScopeElement(this.argName));

            if (this.content is MainCode)
            { 
                textWriter.Write("function({0}) return ", this.argName);
            }
            else
            {
                textWriter.WriteLine("function({0})", this.argName);
            }

            this.content.WriteLua(textWriter, providers);
            textWriter.Write(" end");

            providers.NameProvider.SetScope(originalScope);
        }

        public SyntaxToken Analyze(SyntaxToken token)
        {
            LuaElementHelper.CheckType(typeof(SimpleLambdaExpressionSyntax), token.Parent);
            SyntaxToken prevToken = token.GetPreviousToken();
            LuaElementHelper.CheckType(typeof(ParameterSyntax), prevToken.Parent);
            this.argName = prevToken.Text;

            token = token.GetNextToken();
            if (token.Parent is BlockSyntax)
            {
                this.content = new Block();
                return this.content.Analyze(token);
            }
            else
            { 
                this.content = new MainCode(t => t.Parent is ArgumentListSyntax && t.Text == ")");
                token = this.content.Analyze(token);
                return token.GetPreviousToken(); // resend the ")" to break out of argument list.
            }
            
        }
    }
}