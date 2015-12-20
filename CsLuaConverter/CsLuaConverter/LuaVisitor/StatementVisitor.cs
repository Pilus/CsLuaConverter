namespace CsLuaConverter.LuaVisitor
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections;
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

            for (var i = 0; i < elements.Count; i++)
            {
                var element = elements[i];
                var nextElement = i + 1 < elements.Count ? elements[i + 1] : null;

                if (nextElement is PostIncrementExpression)
                {
                    PostIncrementExpressionVisitor.Visit(nextElement as PostIncrementExpression, textWriter, providers, element);
                    i++;
                }
                else if (nextElement is PostDecrementExpression)
                {
                    PostDecrementExpressionVisitor.Visit(nextElement as PostDecrementExpression, textWriter, providers, element);
                    i++;
                }
                else
                {
                    VisitorList.Visit(element);
                }
            }

            if (statement.EndToken.Equals(";"))
            {
                textWriter.WriteLine(statement.EndToken);
            }
            else if (statement.EndToken.Equals("}"))
            {
                
            }
            else
            {
                textWriter.Write(statement.EndToken);
            }
        }
    }
}