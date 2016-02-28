namespace CsLuaConverter.LuaVisitor
{
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.Linq;
    using CodeElementAnalysis;
    using Providers;

    public class SimpleLambdaExpressionVisitor : IVisitor<SimpleLambdaExpression>
    {
        public void Visit(SimpleLambdaExpression element, IndentedTextWriter textWriter, IProviders providers)
        {
            throw new LuaVisitorException("Use the SimpleLambdaExpression with arg infomation parsed.");
        }

        public static void Visit(IEnumerable<BaseElement> parameters, IEnumerable<BaseElement> body, IndentedTextWriter textWriter, IProviders providers)
        {
            var bodyElements = body.ToList();

            textWriter.Write("function(");
            var first = true;

            foreach (var parameter in parameters)
            {
                if (first)
                {
                    first = false;
                }
                else
                {
                    textWriter.Write(",");
                }

                VisitorList.Visit(parameter);
            }

            textWriter.Write(")");

            if (bodyElements.Count == 1 && bodyElements[0] is Block)
            {
                textWriter.WriteLine("");
                VisitorList.Visit(bodyElements[0]);
            }
            else
            {
                textWriter.Write(" return ");
                bodyElements.ForEach(VisitorList.Visit);
            }

            textWriter.Write(" end");
        }
    }
}