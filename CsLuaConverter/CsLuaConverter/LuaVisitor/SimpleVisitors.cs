namespace CsLuaConverter.LuaVisitor
{
    using System.CodeDom.Compiler;
    using CodeElementAnalysis;
    using Providers;

    public class SimpleVisitors : IVisitor<FalseLiteralExpression>, IVisitor<EqualsValueClause>
    {
        public void Visit(FalseLiteralExpression element, IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.Write("false");
        }

        public void Visit(EqualsValueClause element, IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.Write(" = ");
        }
    }
}