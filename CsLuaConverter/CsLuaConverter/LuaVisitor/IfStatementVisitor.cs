namespace CsLuaConverter.LuaVisitor
{
    using System.CodeDom.Compiler;
    using CodeElementAnalysis;
    using Providers;

    public class IfStatementVisitor : IVisitor<IfStatement>, IVisitor<ElseClause>
    {
        public void Visit(IfStatement element, IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.Write("if (");
            VisitorList.Visit(element.Statement);
            textWriter.WriteLine(") then");
            VisitorList.Visit(element.Block);

            if (element.Else != null)
            {
                VisitorList.Visit(element.Else);
            }
            else
            {
                textWriter.WriteLine("end");
            }
        }

        public void Visit(ElseClause element, IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.WriteLine("else");
            VisitorList.Visit(element.Block);
            textWriter.WriteLine("end");
        }
    }
}