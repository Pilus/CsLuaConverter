namespace CsLuaConverter.SyntaxAnalysis
{
    using System.CodeDom.Compiler;
    using Microsoft.CodeAnalysis;
    using Providers;

    internal class Creator : ILuaElement
    {
        private readonly VariableType variableType;
        public Creator(VariableType variableType)
        {
            this.variableType = variableType;
        }

        public void WriteLua(IndentedTextWriter textWriter, IProviders providers)
        {
            if (this.variableType != null)
            {
                textWriter.Write("({0}).__Cstor", this.variableType.GetQuotedGenericTypeString(providers) ?? "");
            }
            else
            {
                textWriter.Write("().__Cstor");
            }
        }

        public SyntaxToken Analyze(SyntaxToken token)
        {
            throw new System.NotImplementedException();
        }
    }
}