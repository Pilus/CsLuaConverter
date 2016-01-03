namespace CsLuaConverter.LuaVisitor
{
    using System.CodeDom.Compiler;
    using CodeElementAnalysis;
    using Providers;

    public class IsExpressionVisitor
    {
        public static void Visit(IsExpression element, IndentedTextWriter textWriter, IProviders providers, BaseElement comparedElement, IdentifierName typeElement)
        {
            VisitorList.Visit(typeElement);
            textWriter.Write(".__is(");
            VisitorList.Visit(comparedElement);
            textWriter.Write(")");
        }
    }
}