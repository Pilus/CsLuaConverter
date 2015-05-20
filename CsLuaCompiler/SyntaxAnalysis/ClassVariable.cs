namespace CsLuaCompiler.SyntaxAnalysis
{
    using System.CodeDom.Compiler;
    using ClassElements;
    using CsLuaCompiler.Providers;
    using CsLuaCompiler.Providers.TypeProvider;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    internal class ClassVariable : ILuaElement
    {
        

        public ILuaElement Expression;
        public string Name;
        public Scope Scope = Scope.Private;
        public VariableType Type;
        private bool isStatic;

        private readonly string className;

        public ClassVariable(string className)
        {
            this.className = className;
        }

        public void WriteLua(IndentedTextWriter textWriter, IProviders providers)
        {
            if (this.Expression != null)
            {
                LuaFormatter.WriteClassElement(textWriter, ElementType.Variable, this.Name, this.isStatic, false, 
                    () => this.Expression.WriteLua(textWriter, providers), this.className);
            }
            else
            {
                string typeName = this.Type.GetTypeString();
                LuaFormatter.WriteClassElement(textWriter, ElementType.Variable, this.Name, this.isStatic,
                    string.Format("__GetDefaultValue(\"{0}\", {1}, generics)", typeName, this.Type.IsNullable ? "true" : "false"), this.className);
            }
        }

        public SyntaxToken Analyze(SyntaxToken token)
        {
            this.Scope = (Scope) System.Enum.Parse(typeof(Scope), LuaElementHelper.UppercaseFirst(token.Text));
                // private or public
            token = token.GetNextToken();

            if (token.Text == "static")
            {
                this.isStatic = true;
                token = token.GetNextToken();
            }
            else if (token.Text == "const")
            {
                this.isStatic = true;
                token = token.GetNextToken();
            }

            while (true)
            {
                this.Type = new VariableType();
                token = this.Type.Analyze(token);
                token = token.GetNextToken();

                if (token.Parent is NullableTypeSyntax)
                {
                    token = token.GetNextToken();
                }

                if (token.Parent is QualifiedNameSyntax)
                {
                    token = token.GetNextToken();
                }
                else
                {
                    break;
                }
            }

            LuaElementHelper.CheckType(typeof(VariableDeclaratorSyntax), token.Parent);
            this.Name = token.Text;

            token = token.GetNextToken();
            if (token.Parent is EqualsValueClauseSyntax) // =
            {
                token = token.GetNextToken();
                this.Expression = new MainCode(t => t.Parent is FieldDeclarationSyntax && t.Text == ";");
                token = this.Expression.Analyze(token);
            }

            LuaElementHelper.CheckType(typeof(FieldDeclarationSyntax), token.Parent); // ;
            return token;
        }

        public string GetMapping()
        {
            return this.Scope.Equals(Scope.Private) ? "private" : "public";
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