namespace CsLuaConverter.LuaVisitor
{
    using System.CodeDom.Compiler;
    using CodeElementAnalysis;
    using Providers;

    public class ComplexElementInitializerExpressionVisitor : IVisitor<ComplexElementInitializerExpression>
    {
        public void Visit(ComplexElementInitializerExpression element, IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.Write("[");
            VisitorList.Visit(element.KeyElement);
            textWriter.Write("] = ");
            VisitorList.Visit(element.ValueElement);
        }
    }
}