namespace CsLuaConverter.LuaVisitor
{
    using System;
    using System.CodeDom.Compiler;
    using CodeElementAnalysis;
    using Providers;

    public class ThrowStatementVisitor : IVisitor<ThrowStatement>
    {
        public void Visit(ThrowStatement element, IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.Write("_M.Throw(");
            element.Statement.EndToken = String.Empty;
            VisitorList.Visit(element.Statement);
            textWriter.Write(")");
        }
    }
}