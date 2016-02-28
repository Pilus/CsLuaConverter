namespace CsLuaConverter.LuaVisitor
{
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.Linq;
    using CodeElementAnalysis;
    using Providers;

    public class ParenthesizedLambdaExpressionVisitor
    {
        public static void Visit(ParenthesizedLambdaExpression element, IndentedTextWriter textWriter,
            IProviders providers, ParameterList args, IEnumerable<BaseElement> preceedingElements)
        {
            textWriter.Write("function(");
            VisitorList.Visit(args);
            textWriter.WriteLine(")");

            var content = preceedingElements.Single() as Block;
            VisitorList.Visit(content);
            textWriter.WriteLine("end");
        }
    }
}