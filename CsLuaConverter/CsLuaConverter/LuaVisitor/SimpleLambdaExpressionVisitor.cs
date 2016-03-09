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

        public static void Visit(IEnumerable<BaseElement> parameters, BaseElement[] body, IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.Write("_M.LB(function(");
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

            if (body.Length == 1 && body[0] is Block)
            {
                textWriter.WriteLine("");
                VisitorList.Visit(body[0]);
            }
            else
            {
                textWriter.Write(" return ");
                foreach (var b in body)
                {
                    VisitorList.Visit(b);
                }
            }

            textWriter.Write(" end");
            WriteLambdaTypes(parameters, body, textWriter, providers);
            textWriter.Write(")");
        }

        private static void WriteLambdaTypes(IEnumerable<BaseElement> parameters, BaseElement[] body, IndentedTextWriter textWriter, IProviders providers)
        {
            string returnType = null;
            if (body.Length == 1 && body[0] is Block)
            {
                throw new System.NotImplementedException();
            }
            else
            {
                returnType = providers.TypeKnowledgeRegistry.Get(body.First()).ToString();
            }

            if (string.IsNullOrEmpty(returnType))
            {
                textWriter.Write(",nil");
            }
            else
            {
                textWriter.Write("," + returnType);
            }
        }
    }
}