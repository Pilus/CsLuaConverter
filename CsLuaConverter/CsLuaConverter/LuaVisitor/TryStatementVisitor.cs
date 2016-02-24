namespace CsLuaConverter.LuaVisitor
{
    using System.CodeDom.Compiler;
    using CodeElementAnalysis;
    using Providers;
    using Providers.TypeProvider;

    public class TryStatementVisitor : IVisitor<TryStatement>
    {
        public void Visit(TryStatement element, IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.WriteLine("_M.Try(");
            textWriter.Indent++;

            textWriter.WriteLine("function()");
            VisitorList.Visit(element.TryBlock);
            textWriter.WriteLine("end,");
            textWriter.WriteLine("{");
            textWriter.Indent++;

            foreach (var pair in element.CatchPairs)
            {
                textWriter.WriteLine("{");
                textWriter.Indent++;

                var variableName = string.Empty;

                if (pair.Declaration != null)
                {
                    textWriter.Write("type = ");
                    VisitorList.Visit(pair.Declaration.ExceptionType);
                    variableName = pair.Declaration.ExceptionVariableName;
                    textWriter.WriteLine(".__typeof,");
                }

                textWriter.WriteLine("func = function({0})", variableName);

                var scope = providers.NameProvider.CloneScope();
                if (!string.IsNullOrEmpty(variableName))
                {
                    providers.NameProvider.AddToScope(new ScopeElement(variableName));
                }

                VisitorList.Visit(pair.Block);
                
                providers.NameProvider.SetScope(scope);

                textWriter.WriteLine("end,");
                textWriter.Indent--;
                textWriter.WriteLine("},");
            }
            textWriter.Indent--;
            textWriter.WriteLine("},");

            if (element.FinallyBlock != null)
            {
                textWriter.WriteLine("function()");
                VisitorList.Visit(element.FinallyBlock);
                textWriter.WriteLine("end");
            }
            else
            {
                textWriter.WriteLine("nil");
            }

            textWriter.Indent--;
            textWriter.WriteLine(");");

        }
    }
}