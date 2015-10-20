namespace CsLuaConverter.SyntaxAnalysis
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using ClassElements;
    using CsLuaConverter.Providers;
    using CsLuaConverter.Providers.TypeProvider;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    internal class ClassVariable : ILuaElement, IClassMember
    {
        public ILuaElement Expression;
        public string Name { get; private set; }
        public Scope Scope { get; private set; }
        public bool Static { get { return this.isStatic; } }

        public string MemberType { get { return "Variable"; } }

        public VariableDefinition Type;
        private bool isStatic;

        private readonly string className;

        public ClassVariable(string className)
        {
            this.Scope = Scope.Private;
            this.className = className;
        }

        public void AddValues(Dictionary<string, object> values, IndentedTextWriter textWriter, IProviders providers)
        {
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
                LuaFormatter.WriteClassElement(textWriter, ElementType.Variable, this.Name, this.isStatic,
                    string.Format("CsLuaMeta.GetDefaultValue({0}, {1}, generics)", this.Type.GetQuotedFullTypeString(providers), this.Type.IsNullable ? "true" : "false"), this.className);
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
                this.Type = new VariableDefinition();
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
                ClassPrefix = "element.",
            };
        }

        
    }
}