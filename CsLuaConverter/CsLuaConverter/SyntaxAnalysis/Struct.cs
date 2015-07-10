namespace CsLuaConverter.SyntaxAnalysis
{
    using System.CodeDom.Compiler;
    using System.Linq;
    using CsLuaConverter.Providers;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    internal class Struct : ILuaElement
    {
        private string typeName;
        
        public void WriteLua(IndentedTextWriter textWriter, IProviders providers)
        {
            throw new ConverterException("Structs are currently not supported.");
            /*
            var type = providers.TypeProvider.LookupType(this.typeName).Type;
            var implements = type.GetInterfaces()
                .Select(i => QuoteString(i.Name)).ToList();

            if (type.BaseType != null && !type.BaseType.Name.Equals("ValueType"))
            {
                implements.Add(QuoteString(type.BaseType.Name));
            }

            textWriter.WriteLine(
                "{0} = __Struct({1}, {{{2}}}),", this.typeName, 
                QuoteString(type.Name),
                string.Join(",", implements)); */
        }   

        public SyntaxToken Analyze(SyntaxToken token)
        {
            LuaElementHelper.CheckType(typeof(StructDeclarationSyntax), token.Parent);
            token = token.GetNextToken();
            LuaElementHelper.CheckType(typeof(StructDeclarationSyntax), token.Parent);
            token = token.GetNextToken();
            LuaElementHelper.CheckType(typeof(StructDeclarationSyntax), token.Parent);
            this.typeName = token.Text;

            while (!(token.Parent is StructDeclarationSyntax && token.Text == "}"))
            {
                token = token.GetNextToken();
            }

            return token;
        }
    }
}