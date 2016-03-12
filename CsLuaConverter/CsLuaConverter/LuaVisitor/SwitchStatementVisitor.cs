namespace CsLuaConverter.LuaVisitor
{
    using System.CodeDom.Compiler;
    using System.Linq;
    using System.Security.Principal;
    using CodeElementAnalysis;
    using CodeElementAnalysis.Statements;
    using Providers;

    public class SwitchStatementVisitor : IVisitor<SwitchStatement>
    {
        public void Visit(SwitchStatement element, IndentedTextWriter textWriter, IProviders providers)
        {
            var numElements = element.SwitchElements.Count;
            var i = 0;
            while (i < numElements)
            {
                var e = element.SwitchElements[i];
                if (i == 0)
                {
                    if (e.Default)
                    {
                        textWriter.Write("if (true");
                    }
                    else
                    {
                        textWriter.Write("if (");
                        VisitorList.Visit(element.SwitchTarget);
                        textWriter.Write("==");
                        VisitorList.Visit(e.Identifier);
                    }
                }
                else
                {
                    if (e.Default)
                    {
                        textWriter.WriteLine("else");
                    }
                    else
                    {
                        textWriter.Write("elseif (");
                        VisitorList.Visit(element.SwitchTarget);
                        textWriter.Write("==");
                        VisitorList.Visit(e.Identifier);
                    }
                }

                while (e.Statements.Count == 0)
                {
                    i++;
                    e = element.SwitchElements[i];
                    textWriter.Write(" or ");

                    if (e.Default)
                    {
                        textWriter.Write("true");
                    }
                    else
                    {
                        VisitorList.Visit(element.SwitchTarget);
                        textWriter.Write("==");
                        VisitorList.Visit(e.Identifier);
                    }
                }

                if (!(e.Default && i > 0 && element.SwitchElements[i-1].Statements.Count > 0))
                {
                    textWriter.WriteLine(") then");
                }

                textWriter.Indent++;

                foreach (var subElement in e.Statements)
                {
                    if (!(subElement is BreakStatement))
                    {
                        VisitorList.Visit(subElement);
                    }
                }

                textWriter.Indent--;

                i++;
            }

            textWriter.WriteLine("end");
        }
    }
}