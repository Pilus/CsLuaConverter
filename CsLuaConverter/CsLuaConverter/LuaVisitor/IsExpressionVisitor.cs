namespace CsLuaConverter.LuaVisitor
{
    using System;
    using System.CodeDom.Compiler;
    using CodeElementAnalysis;
    using Providers;

    public class IsExpressionVisitor : IVisitor<IsExpression>
    {
        public static void Visit(IsExpression element, IndentedTextWriter textWriter, IProviders providers, BaseElement comparedElement, BaseElement typeElement)
        {
            VisitorList.Visit(typeElement);
            textWriter.Write(".__is(");
            VisitorList.Visit(comparedElement);
            textWriter.Write(")");
        }

        public void Visit(IsExpression element, IndentedTextWriter textWriter, IProviders providers)
        {
            throw new LuaVisitorException("Invoke the special Visitor method for IsExpression with the compared element and type element.");
        }
    }
}