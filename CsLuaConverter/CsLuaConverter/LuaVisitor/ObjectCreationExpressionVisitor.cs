namespace CsLuaConverter.LuaVisitor
{
    using System.CodeDom.Compiler;
    using CodeElementAnalysis;
    using Providers;

    public class ObjectCreationExpressionVisitor : IVisitor<ObjectCreationExpression>
    {
        public void Visit(ObjectCreationExpression element, IndentedTextWriter textWriter, IProviders providers)
        {
            VisitorList.Visit(element.TypeElement);
            VisitorList.Visit(element.ArgumentList);
        }
    }
}