namespace CsLuaConverter.CodeElementAnalysis
{
    using Microsoft.CodeAnalysis;

    public class BreakStatement : BaseElement
    {
        public override SyntaxToken Analyze(SyntaxToken token)
        {
            return token;
        }
    }
}