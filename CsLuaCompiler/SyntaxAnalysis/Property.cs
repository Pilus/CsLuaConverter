namespace CsToLua.SyntaxAnalysis
{
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using ClassElements;
    using CsLuaCompiler.SyntaxAnalysis.NameAndTypeProvider;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    internal class Property : ILuaElement
    {
        public string Name;
        public VariableType Type;
        private Block getBlock;
        private bool isStatic;
        private Scope scope;
        private Block setBlock;
        private bool useDefaultGet;
        private bool useDefaultSet;

        private readonly string className;

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
                    var typeName = this.Type.GetTypeString();
                    LuaFormatter.WriteClassElement(textWriter, ElementType.PropertyGet, this.Name, this.isStatic, providers.DefaultValueProvider.GetDefaultValue(typeName, this.Type.IsNullable), this.className);
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
                    LuaFormatter.WriteClassElement(textWriter, ElementType.PropertySet, this.Name, this.isStatic, "nil", this.className);
                }
                else
                {
                    LuaFormatter.WriteClassElement(textWriter, ElementType.PropertySet, this.Name, this.isStatic, false, () =>
                    {
                        textWriter.WriteLine("function(value)", this.Name);
                        textWriter.Indent++;
                        List<ScopeElement> variableScope = providers.NameProvider.CloneScope();
                        providers.NameProvider.AddToScope(new ScopeElement("value"));
                        this.getBlock.WriteLua(textWriter, providers);
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
            this.scope = (Scope) System.Enum.Parse(typeof(Scope), LuaElementHelper.UppercaseFirst(token.Text));
            token = token.GetNextToken();
            if (token.Text.Equals("static"))
            {
                this.isStatic = true;
                token = token.GetNextToken();
            }
            this.Type = new VariableType(); // return type
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
                ClassPrefix = "class.",
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