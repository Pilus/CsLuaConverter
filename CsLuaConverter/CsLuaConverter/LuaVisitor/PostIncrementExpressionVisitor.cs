namespace CsLuaConverter.LuaVisitor
{
    using System.CodeDom.Compiler;
    using CodeElementAnalysis;
    using Providers;

    public class PostIncrementExpressionVisitor : IVisitor<PostIncrementExpression>
    {
        public void Visit(PostIncrementExpression element, IndentedTextWriter textWriter, IProviders providers)
        {
            throw new LuaVisitorException("Cannot visit PostIncrementExpression without a target element");
        }

        public static void Visit(PostIncrementExpression element, IndentedTextWriter textWriter, IProviders providers, BaseElement targetElement)
        {
            VisitorList.Visit(targetElement);
            textWriter.Write(" = ");
            VisitorList.Visit(targetElement);
            textWriter.Write(" + 1");
        }
    }
}