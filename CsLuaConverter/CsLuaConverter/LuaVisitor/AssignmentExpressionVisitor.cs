namespace CsLuaConverter.LuaVisitor
{
    using System.CodeDom.Compiler;
    using CodeElementAnalysis;
    using Providers;

    public class AssignmentExpressionVisitor
    {
        public static void Visit(SubtractAssignmentExpression element, IndentedTextWriter textWriter, IProviders providers, BaseElement targetElement)
        {
            VisitorList.Visit(targetElement);
            textWriter.Write(" = ");
            VisitorList.Visit(targetElement);
            textWriter.Write(" - ");
        }

        public static void Visit(AddAssignmentExpression element, IndentedTextWriter textWriter, IProviders providers, BaseElement targetElement)
        {
            VisitorList.Visit(targetElement);
            textWriter.Write(" = ");
            VisitorList.Visit(targetElement);
            textWriter.Write(" +_M.Add+ ");
        }
    }
}