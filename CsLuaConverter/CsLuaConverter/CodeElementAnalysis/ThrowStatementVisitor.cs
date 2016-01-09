namespace CsLuaConverter.CodeElementAnalysis
{
    using System.CodeDom.Compiler;
    using LuaVisitor;
    using Providers;

    public class ThrowStatementVisitor : IVisitor<ThrowStatement>
    {
        public void Visit(ThrowStatement element, IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.Write("_M.Throw(");
            VisitorList.Visit(element.Statement);
            textWriter.Write(")");
        }
    }
}