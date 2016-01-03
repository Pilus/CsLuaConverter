namespace CsLuaConverter.LuaVisitor
{
    using System.CodeDom.Compiler;
    using CodeElementAnalysis;
    using Providers;

    public class ObjectInitializerExpressionVisitor : IVisitor<ObjectInitializerExpression>
    {
        public void Visit(ObjectInitializerExpression element, IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.Write(".__Initialize({");

            var first = true;

            foreach (var pair in element.Pairs)
            {
                if (first)
                {
                    first = false;
                }
                else
                {
                    textWriter.Write(",");
                }

                IdentifierNameVisitor.Visit(pair.Name, textWriter, providers, true);
                textWriter.Write(" = ");
                VisitorList.Visit(pair.Statement);
            }

            textWriter.Write("})");
        }
    }
}