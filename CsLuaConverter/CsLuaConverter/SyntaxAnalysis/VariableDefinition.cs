namespace CsLuaConverter.SyntaxAnalysis
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.Linq;
    using CsLuaConverter.Providers;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp.Syntax;
    using Providers.GenericsRegistry;

    [Obsolete("User type reference instead.")]
    internal class VariableDefinition : ILuaElement
    {
        private readonly bool includeVariableName;
        public List<VariableDefinition> Generics;
        public bool initializationCall;
        private bool isArray;
        private string type;
        private VariableName variable;
        public bool IsNullable;

        public VariableDefinition()
        {
            this.includeVariableName = false;
            this.initializationCall = false;
        }

        public VariableDefinition(bool includeVariableName)
        {
            this.includeVariableName = includeVariableName;
            this.initializationCall = false;
        }

        public VariableDefinition(bool includeVariableName, bool initializationCall)
        {
            this.includeVariableName = includeVariableName;
            this.initializationCall = initializationCall;
        }

        public void WriteLua(IndentedTextWriter textWriter, IProviders providers)
        {
            if (this.variable != null)
            {
                if (this.initializationCall)
                {
                    textWriter.Write("local ");
                    this.variable.WriteLua(textWriter, providers);
                    return;
                }
                throw new Exception("Unknown scenario.");
            }

            if (providers.GenericsRegistry.IsGeneric(this.type))
            {
                throw new Exception("Attempting to write type for generic");
            }

            textWriter.Write(providers.TypeProvider.LookupType(this.type));
        }

        public SyntaxToken Analyze(SyntaxToken token)
        {
            if ((token.Parent is FieldDeclarationSyntax || token.Parent is PropertyDeclarationSyntax) && token.Text == "new")
            {
                token = token.GetNextToken();
            }

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
            
            if (token.Parent is TypeArgumentListSyntax && token.Text.Equals("<")) // <
            {
                this.Generics = new List<VariableDefinition>();
                while (!(token.Parent is TypeArgumentListSyntax && token.Text == ">"))
                {
                    token = token.GetNextToken();
                    var generic = new VariableDefinition();
                    token = generic.Analyze(token);
                    token = token.GetNextToken();
                    this.Generics.Add(generic);
                }
                token = token.GetNextToken();
            }

            while (token.Parent is ArrayRankSpecifierSyntax)
            {
                token = token.GetNextToken();
                this.isArray = true;
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

        public bool IsGeneric(IProviders providers)
        {
            return providers.GenericsRegistry.IsGeneric(this.type);
        }


        private string GetBasicType()
        {
            switch (this.type)
            {
                case "object":
                case "bool":
                case "double":
                case "int":
                case "string":
                case "long":
                    return this.type;
                default:
                    return null;
            }
        }

        public string GetFullTypeName(IProviders providers)
        {
            if (this.variable != null)
            {
                throw new Exception("Unknown scenario");
            }
            else if (this.IsGeneric(providers))
            {
                throw new ConverterException("Cannot get full type name of generic.");
            }

            //return this.isArray ? "System.Array" : this.GetBasicType() ?? providers.TypeProvider.LookupType(this.type).ToString();
            return this.isArray ? "System.Array" : providers.TypeProvider.LookupType(this.type).ToString();
        }

        public string GetTypeString()
        {
            string s = this.type;

            if (this.isArray)
            {
                s = "System.Array";
            }

            return s;
        }

        public string GetQuotedTypeString()
        {
            return '"' + this.GetTypeString() + '"';
        }

        public string GetQuotedFullTypeString(IProviders providers)
        {
            if (this.IsGeneric(providers))
            {
                if (providers.GenericsRegistry.GetGenericScope(this.type) == GenericScope.Class)
                {
                    return "generics[genericsMapping['" + this.type + "']]";
                }
                return "methodGenerics['" + this.type + "']";
            }

            return '"' + this.GetFullTypeName(providers) + '"';
        }

        public string GetTypeReferences(IProviders providers)
        {
            if (this.IsGeneric(providers))
            {
                if (providers.GenericsRegistry.GetGenericScope(this.type) == GenericScope.Class)
                {
                    return "generics[genericsMapping['" + this.type + "']]";
                }
                return "methodGenerics['" + this.type + "']";
            }

            return this.GetFullTypeName(providers) + ".__typeof";
        }


        public string GetGenericsList(IProviders providers)
        {
            if (this.isArray)
            {
                return "CsLuaMeta.GenericsList(CsLuaMeta.Generic(\"" + (this.GetBasicType() ?? providers.TypeProvider.LookupType(this.type).ToString()) + "\",nil))"; // TODO: Generic
            }


            if (this.Generics == null)
            {
                return null;
            }
            return "CsLuaMeta.GenericsList(" + string.Join(",", this.Generics.Select(t => "CsLuaMeta.Generic(" + t.GetQuotedFullTypeString(providers) + ")")) + ")";
        }
    }
}