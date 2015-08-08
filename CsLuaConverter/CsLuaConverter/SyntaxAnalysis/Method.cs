namespace CsLuaConverter.SyntaxAnalysis
{
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.Linq;
    using CsLuaConverter.Providers;
    using CsLuaConverter.Providers.TypeProvider;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    internal class Method : IFunction
    {
        public bool IsAbstract;
        public bool IsVirtual;
        public bool IsOverride;
        public string Name;
        public Scope Scope;
        public bool Static;
        private VariableType TypeName;
        private Block block;
        private ParameterList parameters;


        public void WriteLua(IndentedTextWriter textWriter, IProviders providers)
        {
            List<ScopeElement> scopeBefore = providers.NameProvider.CloneScope();

            textWriter.Write("function(");
            this.parameters.WriteLua(textWriter, providers);

            if (this.parameters.LastParameterHasParamKeyword())
            {
                textWriter.WriteLine(",...)");
                var last = (Parameter) this.parameters.Parameters.Last();
                textWriter.Indent++;
                textWriter.WriteLine("if not(type({0}) == 'table' and {0}.__fullTypeName == 'System.Array') or select('#', ...) > 0 then {0} = System.Array(CsLuaMeta.Generic({1},{2})).__Cstor({{{0},...}}); end", last.Name, last.Type.GetQuotedFullTypeString(providers), last.Type.GetGenericsList(providers) ?? "nil");
            }
            else
            {
                textWriter.WriteLine(")");
                textWriter.Indent++;
            }
            
            this.block.WriteLua(textWriter, providers);
            textWriter.Indent--;
            textWriter.Write("end");

            providers.NameProvider.SetScope(scopeBefore);
        }

        public ParameterList GetParameters()
        {
            return this.parameters;
        }

        public SyntaxToken Analyze(SyntaxToken token)
        {
            LuaElementHelper.CheckType(typeof(MethodDeclarationSyntax), token.Parent);


            if (token.Text.Equals("static"))
            {
                this.Static = true;
                token = token.GetNextToken();
            }
            if (token.Parent is MethodDeclarationSyntax)
            {
                string text = LuaElementHelper.UppercaseFirst(token.Text);
                if (text == "Protected")
                {
                    text = "Private";
                }
                this.Scope = (Scope) System.Enum.Parse(typeof(Scope), text);
                token = token.GetNextToken();
            }
            if (token.Text.Equals("static"))
            {
                this.Static = true;
                token = token.GetNextToken();
            }
            if (token.Text.Equals("virtual"))
            {
                this.IsVirtual = true;
                token = token.GetNextToken();
            }
            if (token.Text.Equals("abstract"))
            {
                this.IsAbstract = true;
                token = token.GetNextToken();
            }
            if (token.Text.Equals("override"))
            {
                this.IsOverride = true;
                token = token.GetNextToken();
            }

            this.TypeName = new VariableType();
            token = this.TypeName.Analyze(token);
            token = token.GetNextToken();

            LuaElementHelper.CheckType(typeof(MethodDeclarationSyntax), token.Parent);
            this.Name = token.Text;
            token = token.GetNextToken();

            this.parameters = new ParameterList();
            token = this.parameters.Analyze(token);
            token = token.GetNextToken();

            if (token.Text == ";")
            {
                return token;
            }

            this.block = new Block();
            token = this.block.Analyze(token);

            return token;
        }

        public ScopeElement GetScopeElement()
        {
            return new ScopeElement(this.Name)
            {
                IsFromClass = true,
                ClassPrefix = "class.",
            };
        }
    }
}