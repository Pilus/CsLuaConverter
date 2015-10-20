namespace CsLuaConverter.SyntaxAnalysis
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.Linq;
    using ClassElements;
    using CsLuaConverter.Providers;
    using CsLuaConverter.Providers.TypeProvider;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp.Syntax;
    using CsLuaConverter.Providers.GenericsRegistry;

    internal class Method : IFunction, IClassMember
    {
        public bool IsAbstract;
        public bool IsVirtual;
        public bool IsOverride;
        public string Name { get; private set; }
        public Scope Scope { get; private set; }
        public bool Static { get; private set; }
        public void AddValues(Dictionary<string, object> values, IndentedTextWriter textWriter, IProviders providers)
        {
            if (this.parameters.Generics != null)
            {
                values["hasMethodGenerics"] = true;
                providers.GenericsRegistry.SetGenerics(this.parameters.Generics.Names, GenericScope.Method);
            }

            values["types"] = new Action(() => { textWriter.Write("{{{0}}}", this.parameters.TypesAsReferences(providers)); });
            values["func"] = new Action(() => { this.WriteLua(textWriter, providers); });
        }

        public string MemberType => "Method";

        private VariableDefinition TypeName;
        private Block block;
        private ParameterList parameters;


        public void WriteLua(IndentedTextWriter textWriter, IProviders providers)
        {
            List<ScopeElement> scopeBefore = providers.NameProvider.CloneScope();

            textWriter.Write("function(element");
            if (this.parameters.Generics != null)
            {
                textWriter.Write(",methodGenerics");
            }

            if (this.parameters.Parameters.Count > 0)
            {
                textWriter.Write(",");
            }

            this.parameters.WriteLua(textWriter, providers);

            if (this.parameters.LastParameterHasParamKeyword())
            {
                textWriter.WriteLine(",...)");
                var last = (Parameter) this.parameters.Parameters.Last();
                textWriter.Indent++;
                textWriter.WriteLine("if not(type({0}) == 'table' and {0}.__fullTypeName == 'System.Array') or select('#', ...) > 0 then {0} = System.Array(CsLuaMeta.Generic({1},{2})).__Cstor({{[0] = {0},...}}); end", last.Name, last.Type.GetQuotedFullTypeString(providers), last.Type.GetGenericsList(providers) ?? "nil");
            }
            else
            {
                textWriter.WriteLine(")");
                textWriter.Indent++;
            }

            if (this.parameters.Generics != null)
            {
                textWriter.Write("local methodGenericMapping = ");
                this.parameters.Generics.WriteLua(textWriter, providers);
                textWriter.WriteLine(";");
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

            if (token.Text.Equals("new"))
            {
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

            this.TypeName = new VariableDefinition();
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

            if (token.Text == "where")
            {
                while (!(token.Parent is BlockSyntax))
                {
                    token = token.GetNextToken();
                }
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
                ClassPrefix = "element.",
            };
        }

        
    }
}