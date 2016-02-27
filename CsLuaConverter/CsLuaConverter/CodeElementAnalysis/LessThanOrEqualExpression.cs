namespace CsLuaConverter.CodeElementAnalysis
{
    using Microsoft.CodeAnalysis;

    public class LessThanOrEqualExpression : BaseElement
    {
        public override SyntaxToken Analyze(SyntaxToken token)
        {
            return token;
        }
    }
}