namespace CsLuaConverter.LuaVisitor
{
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
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

            textWriter.Write(") ");

            foreach (var element in body)
            {
                VisitorList.Visit(element);
            }

            textWriter.Write(" end");
        }
    }
}