namespace CsLuaConverter.LuaVisitor
{
    using System.CodeDom.Compiler;
    using CodeElementAnalysis.Statements;
    using Providers;

    public class ThrowStatementVisitor : IVisitor<ThrowStatement>
    {
        public void Visit(ThrowStatement element, IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.Write("_M.Throw(");
            VisitorList.Visit(element.Expression);
            textWriter.Write(")");
        }
    }
}