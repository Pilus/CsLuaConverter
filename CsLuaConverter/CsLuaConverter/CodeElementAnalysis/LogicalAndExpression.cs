namespace CsLuaConverter.CodeElementAnalysis
{
    using Microsoft.CodeAnalysis;

    public class LogicalAndExpression : BaseElement
    {
        public override SyntaxToken Analyze(SyntaxToken token)
        {
            return token;
        }
    }
}