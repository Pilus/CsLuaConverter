namespace CsLuaConverter.CodeElementAnalysis
{
    using Microsoft.CodeAnalysis;

    public class GreaterThanOrEqualExpression : BaseElement
    {
        public override SyntaxToken Analyze(SyntaxToken token)
        {
            return token;
        }
    }
}