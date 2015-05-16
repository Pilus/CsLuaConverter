namespace CsToLua.SyntaxAnalysis
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    internal class VariableType : ILuaElement
    {
        private readonly bool includeVariableName;
        private List<VariableType> generics;
        public bool initializationCall;
        private bool isArray;
        private string type;
        private VariableName variable;
        public bool IsNullable;

        public VariableType()
        {
            this.includeVariableName = false;
            this.initializationCall = false;
        }

        public VariableType(bool includeVariableName)
        {
            this.includeVariableName = includeVariableName;
            this.initializationCall = false;
        }

        public VariableType(bool includeVariableName, bool initializationCall)
        {
            this.includeVariableName = includeVariableName;
            this.initializationCall = initializationCall;
        }

        public void WriteLua(IndentedTextWriter textWriter, FullNameProvider nameProvider)
        {
            if (this.variable != null)
            {
                if (this.initializationCall)
                {
                    textWriter.Write("local ");
                    this.variable.WriteLua(textWriter, nameProvider);
                    return;
                }
                throw new Exception("Unknown scenario.");
            }

            if (this.initializationCall)
            {
                textWriter.Write(nameProvider.LoopupFullName(this.GetTypeString()));
                return;
            }
            textWriter.Write(nameProvider.LoopupFullName(this.type));
        }

        public SyntaxToken Analyze(SyntaxToken token)
        {
            if (token.Parent is FieldDeclarationSyntax && token.Text == "readonly")
            {
                token = token.GetNextToken();
            }

            if (token.Parent is FieldDeclarationSyntax && token.Text == "const")
            {
                token = token.GetNextToken();
            }

            if (token.Parent is FieldDeclarationSyntax && token.Text == "static")
            {
                token = token.GetNextToken();
            }

            if (token.Parent is PropertyDeclarationSyntax && token.Text == "abstract")
            {
                token = token.GetNextToken();
            }

            if (token.Parent is PropertyDeclarationSyntax && token.Text == "virtual")
            {
                token = token.GetNextToken();
            }

            if (token.Parent is PropertyDeclarationSyntax && token.Text == "override")
            {
                token = token.GetNextToken();
            }

            LuaElementHelper.CheckType(
                new[]
                {
                    typeof(IdentifierNameSyntax), typeof(PredefinedTypeSyntax), typeof(GenericNameSyntax),
                    typeof(TypeParameterSyntax)
                },
                token.Parent);
            this.type = token.Text;
            token = token.GetNextToken();

            while (token.Parent is ArrayRankSpecifierSyntax)
            {
                token = token.GetNextToken();
                this.isArray = true;
            }

            if (token.Parent is TypeArgumentListSyntax && token.Text.Equals("<")) // <
            {
                this.generics = new List<VariableType>();
                while (!(token.Parent is TypeArgumentListSyntax && token.Text == ">"))
                {
                    token = token.GetNextToken();
                    var generic = new VariableType();
                    token = generic.Analyze(token);
                    token = token.GetNextToken();
                    this.generics.Add(generic);
                }
                token = token.GetNextToken();
            }

            if (token.Parent is NullableTypeSyntax) // 'type?'
            {
                this.IsNullable = true;
                token = token.GetNextToken();
            }

            if (!(token.Parent is VariableDeclaratorSyntax && this.includeVariableName))
            {
                return token.GetPreviousToken();
            }
            this.variable = new VariableName(false, true, false);
            token = this.variable.Analyze(token);

            return token;
        }

        public string GetFullTypeName(FullNameProvider nameProvider)
        {
            if (this.variable != null)
            {
                throw new Exception("Unknown scenario");
            }
            string typeName = this.GetTypeString();

            switch (typeName)
            {
                case "bool":
                case "double":
                case "int":
                case "string":
                    return typeName;
                default:
                    break;
            }

            return nameProvider.LoopupFullName(typeName);
        }

        public string GetTypeString()
        {
            if (this.isArray)
            {
                return "Array<" + this.type + ">";
            }

            string s = this.type;
            if (this.generics != null)
            {
                //s += "<" + string.Join(",", this.innerTypes.Select( t => t.GetTypeString())) + ">";
                s += "`" + this.generics.Count;
            }

            return s;
        }

        public string GetQuotedTypeString()
        {
            return '"' + this.GetTypeString() + '"';
        }

        public string GetQuotedFullTypeString(FullNameProvider nameProvider)
        {
            return '"' + this.GetFullTypeName(nameProvider) + '"';
        }

        public string GetQuotedGenericTypeString()
        {
            if (this.generics == null)
            {
                return null;
            }
            return "{" + string.Join(",", this.generics.Select(t => '"' + t.GetTypeString() + '"')) + "}";
        }
    }
}