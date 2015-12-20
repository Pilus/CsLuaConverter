namespace CsLuaConverter.LuaVisitor
{
    using System;
    using System.CodeDom.Compiler;
    using CodeElementAnalysis;
    using CsLuaConverter.Providers;

    public class PostDecrementExpressionVisitor : IVisitor<PostDecrementExpression
        >
    {
        public void Visit(PostDecrementExpression element, IndentedTextWriter textWriter, IProviders providers)
        {
            throw new LuaVisitorException("Cannot visit PostDecrementExpression without a target element");
        }

        public static void Visit(PostDecrementExpression element, IndentedTextWriter textWriter, IProviders providers, BaseElement targetElement)
        {
            VisitorList.Visit(targetElement);
            textWriter.Write(" = ");
            VisitorList.Visit(targetElement);
            textWriter.Write(" - 1");
        }
    }
}