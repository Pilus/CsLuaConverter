namespace CsLuaConverter.SyntaxAnalysis
{
    using System.CodeDom.Compiler;
    using Microsoft.CodeAnalysis;
    using Providers;

    internal class Creator : ILuaElement
    {
        private readonly VariableDefinition variableType;
        public Creator(VariableDefinition variableType)
        {
            this.variableType = variableType;
        }

        public void WriteLua(IndentedTextWriter textWriter, IProviders providers)
        {
            if (this.variableType != null)
            {
                textWriter.Write("{{{0}}}", this.variableType.GetGenericsList(providers) ?? "");
            }
            else
            {
                textWriter.Write("");
            }
        }

        public SyntaxToken Analyze(SyntaxToken token)
        {
            throw new System.NotImplementedException();
        }
    }
}