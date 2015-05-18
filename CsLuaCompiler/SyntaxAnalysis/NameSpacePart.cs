namespace CsLuaCompiler.SyntaxAnalysis
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.Linq;
    using CsLuaCompiler.Providers;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    internal class NameSpacePart : ILuaElement
    {
        private readonly List<ILuaElement> elements = new List<ILuaElement>();
        private readonly List<string> usings = new List<string>();
        public List<string> FullName = new List<string>();
        public string Name;
        private List<Attribute> currentAttributes = new List<Attribute>();

        public void WriteLua(IndentedTextWriter textWriter, IProviders providers)
        {
            providers.TypeProvider.SetNamespaces(string.Join(".", this.FullName), this.usings);

            foreach (ILuaElement element in this.elements)
            {
                element.WriteLua(textWriter, providers);
            }
        }

        public SyntaxToken Analyze(SyntaxToken token)
        {
            LuaElementHelper.CheckType(typeof(NamespaceDeclarationSyntax), token.Parent);

            token = token.GetNextToken();
            LuaElementHelper.CheckType(typeof(IdentifierNameSyntax), token.Parent);
            this.FullName.Add(token.Text);

            token = token.GetNextToken();
            while (token.Parent is QualifiedNameSyntax && token.Text.Equals("."))
            {
                token = token.GetNextToken();
                LuaElementHelper.CheckType(typeof(IdentifierNameSyntax), token.Parent);
                this.FullName.Add(token.Text);
                token = token.GetNextToken();
            }

            LuaElementHelper.CheckType(typeof(NamespaceDeclarationSyntax), token.Parent);

            token = token.GetNextToken();

            this.Name = this.FullName.Last();
            //this.usings.Add(this.Name);

            return this.AnalyzeContent(token);
        }

        public bool HasStaticClass()
        {
            return this.elements.Any(e => e is Class && (e as Class).IsStatic);
        }

        private SyntaxToken AnalyzeContent(SyntaxToken token)
        {
            while (!(token.Parent is NamespaceDeclarationSyntax))
            {
                switch (token.Parent.GetType().Name)
                {
                    case "UsingDirectiveSyntax":
                        token = this.AnalyzeUsingDirective(token);
                        break;
                    case "ClassDeclarationSyntax":
                        var _class = new Class(this.currentAttributes);
                        this.currentAttributes = new List<Attribute>();
                        token = _class.Analyze(token);
                        this.elements.Add(_class);
                        break;
                    case "AttributeListSyntax":
                        var attribute = new Attribute();
                        token = attribute.Analyze(token);
                        this.currentAttributes.Add(attribute);
                        break;
                    case "EnumDeclarationSyntax":
                        var _enum = new EnumElement();
                        token = _enum.Analyze(token);
                        this.elements.Add(_enum);
                        break;
                    case "StructDeclarationSyntax":
                        var _struct = new Struct();
                        token = _struct.Analyze(token);
                        this.elements.Add(_struct);
                        break;
                    case "InterfaceDeclarationSyntax":
                        var iface = new Interface();
                        token = iface.Analyze(token);
                        break;
                    default:
                        throw new Exception(string.Format("Unexpeted token in namespace: {0}.",
                            token.Parent.GetType().Name));
                }
                token = token.GetNextToken();
            }

            return token;
        }

        private SyntaxToken AnalyzeUsingDirective(SyntaxToken token)
        {
            LuaElementHelper.CheckType(typeof(UsingDirectiveSyntax), token.Parent); // using
            token = token.GetNextToken();
            LuaElementHelper.CheckType(typeof(IdentifierNameSyntax), token.Parent); // x
            string name = token.Text;

            while (token.GetNextToken().Parent is QualifiedNameSyntax)
            {
                token = token.GetNextToken();
                LuaElementHelper.CheckType(typeof(QualifiedNameSyntax), token.Parent); // .
                name += token.Text;

                token = token.GetNextToken();
                LuaElementHelper.CheckType(typeof(IdentifierNameSyntax), token.Parent); // x  
                name += token.Text;
            }
            this.usings.Add(name);

            token = token.GetNextToken();
            if (token.Parent is NameEqualsSyntax) // = 
            {
                token = token.GetNextToken();
                LuaElementHelper.CheckType(typeof(IdentifierNameSyntax), token.Parent); // x

                while (token.GetNextToken().Parent is QualifiedNameSyntax)
                {
                    token = token.GetNextToken();
                    LuaElementHelper.CheckType(typeof(QualifiedNameSyntax), token.Parent); // .
                    token = token.GetNextToken();
                    LuaElementHelper.CheckType(typeof(IdentifierNameSyntax), token.Parent);
                        // x                              
                }
                token = token.GetNextToken();
            }
            LuaElementHelper.CheckType(typeof(UsingDirectiveSyntax), token.Parent); // ;

            return token;
        }

        public void WriteMainCallLua(IndentedTextWriter writer, string nameSpace)
        {
            foreach (ILuaElement element in this.elements)
            {
                if (element is Class)
                {
                    (element as Class).WriteMainCallLua(writer, nameSpace);
                }
            }
        }
    }
}