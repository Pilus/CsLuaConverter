namespace CsLuaConverter.LuaVisitor
{
    using System.CodeDom.Compiler;
    using CodeElementAnalysis.Statements;
    using Providers;

    public class ForStatementVisitor : IVisitor<ForStatement>
    {
        public void Visit(ForStatement element, IndentedTextWriter textWriter, IProviders providers)
        {
            var scope = providers.NameProvider.CloneScope();
            textWriter.Write("local ");
            VisitorList.Visit(element.IteratorName);
            textWriter.Write(" = ");
            VisitorList.Visit(element.StartValue);
            
            textWriter.Write("while (");
            VisitorList.Visit(element.Condition);
            textWriter.WriteLine(") do");
            VisitorList.Visit(element.Block);

            textWriter.Indent++;
            VisitorList.Visit(element.Incrementor);
            textWriter.Indent--;

            textWriter.WriteLine("end");

            providers.NameProvider.SetScope(scope);
        }
    }
}