namespace CsLuaConverter.LuaVisitor
{
    using System.CodeDom.Compiler;
    using CodeElementAnalysis;
    using Providers;

    public class ObjectInitializerExpressionVisitor : IOpenCloseVisitor<ObjectInitializerExpression>
    {
        public void Visit(ObjectInitializerExpression element, IndentedTextWriter textWriter, IProviders providers)
        {
            this.WriteOpen(element, textWriter, providers);
            this.WriteClose(element, textWriter, providers);
        }

        public void WriteOpen(ObjectInitializerExpression element, IndentedTextWriter textWriter, IProviders providers)
        {
            
        }

        public void WriteClose(ObjectInitializerExpression element, IndentedTextWriter textWriter, IProviders providers)
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

                IdentifierNameVisitor.Visit(pair.Name, textWriter, providers, IdentifyerType.AsIs);
                textWriter.Write(" = ");
                VisitorList.Visit(pair.Expression);
            }

            textWriter.Write("})");
        }
    }
}