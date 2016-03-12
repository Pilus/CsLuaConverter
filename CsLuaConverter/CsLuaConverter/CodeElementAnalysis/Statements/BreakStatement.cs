namespace CsLuaConverter.CodeElementAnalysis.Statements
{
    using Microsoft.CodeAnalysis;

    public class BreakStatement : BaseStatement
    {
        public override SyntaxToken Analyze(SyntaxToken token)
        {
            return token;
        }
    }
}