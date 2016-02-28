namespace CsLuaConverter.LuaVisitor
{
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.Linq;
    using CodeElementAnalysis;
    using CodeElementAnalysis.Statements;
    using Providers;

    public class StatementVisitor : IVisitor<Statement>
    {
        public void Visit(Statement statement, IndentedTextWriter textWriter, IProviders providers)
        {
            var elements = statement.ContainedElements.ToList();
            Visit(elements, textWriter, providers);

            if (statement.EndToken.Equals(";"))
            {
                textWriter.WriteLine(statement.EndToken);
            }
        }

        public static void Visit(IList<BaseElement> elements, IndentedTextWriter textWriter, IProviders providers)
        {
            for (var i = 0; i < elements.Count; i++)
            {
                var element = elements[i];
                var nextElement = i + 1 < elements.Count ? elements[i + 1] : null;

                if (i == 0 && (element is PredefinedType && (element as PredefinedType).InnerElement == null))
                {
                    textWriter.Write("local ");
                }
                else if (nextElement is PostIncrementExpression)
                {
                    PostIncrementExpressionVisitor.Visit(nextElement as PostIncrementExpression, textWriter, providers, element);
                    i++;
                }
                else if (nextElement is PostDecrementExpression)
                {
                    PostDecrementExpressionVisitor.Visit(nextElement as PostDecrementExpression, textWriter, providers, element);
                    i++;
                }
                else if (nextElement is IsExpression)
                {
                    IsExpressionVisitor.Visit(nextElement as IsExpression, textWriter, providers, element, elements[i+2]);
                    i+=2;
                }
                else if (nextElement is AsExpression)
                {
                    VisitorList.Visit(element);
                    // Do not perform any casts.
                    i += 2;
                }
                else if (nextElement is SubtractAssignmentExpression)
                {
                    AssignmentExpressionVisitor.Visit(nextElement as SubtractAssignmentExpression, textWriter, providers, element);
                    i++;
                }
                else if (nextElement is AddAssignmentExpression)
                {
                    AssignmentExpressionVisitor.Visit(nextElement as AddAssignmentExpression, textWriter, providers, element);
                    i++;
                }
                else if (nextElement is SimpleMemberAccessExpression)
                {
                    textWriter.Write("(");
                    VisitorList.Visit(element);
                    textWriter.Write(" % _M.DOT)");
                }
                else if (nextElement is ParenthesizedLambdaExpression)
                {
                    ParenthesizedLambdaExpressionVisitor.Visit(nextElement as ParenthesizedLambdaExpression, textWriter, providers, element as ParameterList, elements.Skip(i+2));
                    return;
                }
                else
                {
                    VisitorList.Visit(element);
                }
            }
        }
    }
}