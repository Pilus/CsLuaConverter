namespace CsLuaConverter.CodeElementAnalysis
{
    using Microsoft.CodeAnalysis;

    public class DivideExpression : BaseElement
    {
        public override SyntaxToken Analyze(SyntaxToken token)
        {
            return token;
        }
    }
}