namespace CsLuaConverter.SyntaxAnalysis
{
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using ClassElements;
    using CsLuaConverter.Providers;
    using CsLuaConverter.Providers.TypeProvider;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    internal class Property : ILuaElement, IClassMember
    {
        public string Name { get; private set; }
        public VariableDefinition Type;
        private Block getBlock;
        private bool isStatic;
        private Block setBlock;
        private bool useDefaultGet;
        private bool useDefaultSet;

        private readonly string className;

        public Scope Scope { get; private set; }

        public string MemberType => this.IsDefault() ? "AutoProperty" : "Property";

        public bool Static => this.isStatic;
        public void AddValues(Dictionary<string, object> values, IndentedTextWriter textWriter, IProviders providers)
        {

        }

        public Property(string className)
        {
            this.className = className;
        }

        public void WriteLua(IndentedTextWriter textWriter, IProviders providers)
        {
            if (this.GotGet())
            {
                if (this.useDefaultGet)
                {
                    LuaFormatter.WriteClassElement(textWriter, ElementType.PropertyAuto, this.Name, this.isStatic,
                        string.Format("CsLuaMeta.GetDefaultValue({0}, {1}, generics)", this.Type.GetQuotedFullTypeString(providers), this.Type.IsNullable ? "true" : "false"), this.className);
                }
                else
                {
                    LuaFormatter.WriteClassElement(textWriter, ElementType.PropertyGet, this.Name, this.isStatic, false, () =>
                    {
                        textWriter.WriteLine("function()", this.Name);
                        textWriter.Indent++;
                        List<ScopeElement> variableScope = providers.NameProvider.CloneScope();
                        this.getBlock.WriteLua(textWriter, providers);
                        providers.NameProvider.SetScope(variableScope);
                        textWriter.Indent--;
                        textWriter.Write("end");
                    }, this.className);
                }
            }
            
            if (this.GotSet())
            {
                if (this.useDefaultSet)
                {
                    //LuaFormatter.WriteClassElement(textWriter, ElementType.PropertySet, this.Name, this.isStatic, "nil", this.className);
                }
                else
                {
                    LuaFormatter.WriteClassElement(textWriter, ElementType.PropertySet, this.Name, this.isStatic, false, () =>
                    {
                        textWriter.WriteLine("function(value)", this.Name);
                        textWriter.Indent++;
                        List<ScopeElement> variableScope = providers.NameProvider.CloneScope();
                        providers.NameProvider.AddToScope(new ScopeElement("value"));
                        this.setBlock.WriteLua(textWriter, providers);
                        providers.NameProvider.SetScope(variableScope);
                        textWriter.Indent--;
                        textWriter.Write("end");
                    }, this.className);
                }
            }
        }

        public SyntaxToken Analyze(SyntaxToken token)
        {
            LuaElementHelper.CheckType(typeof(PropertyDeclarationSyntax), token.Parent); // Public 
            if (token.Text.Equals("new"))
            {
                token = token.GetNextToken();
            }
            this.Scope = (Scope) System.Enum.Parse(typeof(Scope), LuaElementHelper.UppercaseFirst(token.Text));
            token = token.GetNextToken();
            if (token.Text.Equals("static"))
            {
                this.isStatic = true;
                token = token.GetNextToken();
            }
            this.Type = new VariableDefinition(); // return type
            token = this.Type.Analyze(token);
            token = token.GetNextToken();
            this.Name = token.Text; // Name
            token = token.GetNextToken();
            LuaElementHelper.CheckType(typeof(AccessorListSyntax), token.Parent); // {
            token = token.GetNextToken();

            while (!(token.Parent is AccessorListSyntax))
            {
                if (token.Parent is AccessorDeclarationSyntax) // get / set or scope
                {
                    if (token.Text == "private" || token.Text == "public" || token.Text == "protected")
                    {
                        token = token.GetNextToken();
                        LuaElementHelper.CheckType(typeof(AccessorDeclarationSyntax), token.Parent);
                    }

                    string getOrSet = token.Text;
                    token = token.GetNextToken();
                    LuaElementHelper.CheckType(new[] {typeof(BlockSyntax), typeof(AccessorDeclarationSyntax)},
                        token.Parent);
                    // { or ;
                    if (token.Parent is BlockSyntax)
                    {
                        var block = new Block();
                        token = block.Analyze(token);

                        if (getOrSet.Equals("get"))
                        {
                            this.getBlock = block;
                        }
                        else
                        {
                            this.setBlock = block;
                        }
                    }
                    else
                    {
                        if (getOrSet.Equals("get"))
                        {
                            this.useDefaultGet = true;
                        }
                        else
                        {
                            this.useDefaultSet = true;
                        }
                    }
                }
                token = token.GetNextToken();
            }
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

        public bool GotSet()
        {
            return this.useDefaultSet || this.setBlock != null;
        }

        public bool GotGet()
        {
            return this.useDefaultGet || this.getBlock != null;
        }

        public bool IsDefault()
        {
            return this.useDefaultGet && this.useDefaultSet;
        }
    }
}