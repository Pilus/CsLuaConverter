namespace CsLuaConverter.LuaVisitor
{
    using System.CodeDom.Compiler;
    using System.Linq;
    using CodeElementAnalysis;
    using CodeElementAnalysis.Statements;
    using Providers;

    public class StatementVisitor : IVisitor<Statement>
    {
        public void Visit(Statement statement, IndentedTextWriter textWriter, IProviders providers)
        {
            var elements = statement.ContainedElements;

            switch (statement.StatementInfo.StatementType)
            {
                case StatementType.VariableDeclaration:
                    textWriter.Write("local ");
                    elements = elements.Skip(1).ToList();
                    break;
            }

            foreach (var containedElement in elements)
            {
                VisitorList.Visit(containedElement);
            }

            if (statement.EndToken.Equals(";"))
            {
                textWriter.WriteLine(statement.EndToken);
            }
            else
            {
                textWriter.Write(statement.EndToken);
            }
        }
    }
}