namespace CsLuaConverter.LuaVisitor
{
    using System.CodeDom.Compiler;
    using CodeElementAnalysis;
    using Providers;

    public class SimpleVisitors : 
        IVisitor<FalseLiteralExpression>, IVisitor<EqualsValueClause>, IVisitor<SimpleMemberAccessExpression>, IVisitor<TrueLiteralExpression>,
        IVisitor<NullLiteralExpression>, IVisitor<SimpleAssignmentExpression>, IVisitor<ThisExpression>, IVisitor<AddExpression>,
        IVisitor<CharacterLiteralExpression>, IVisitor<NotEqualsExpression>, IVisitor<ReturnStatement>, IVisitor<ParenthesizedExpression>, 
        IVisitor<EqualsExpression>, IVisitor<BaseExpression>, IVisitor<CastExpression>, IVisitor<LogicalNotExpression>
    {
        public void Visit(FalseLiteralExpression element, IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.Write("false");
        }

        public void Visit(EqualsValueClause element, IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.Write(" = ");
        }

        public void Visit(SimpleMemberAccessExpression element, IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.Write(".");
            if (element.InnerElement is IdentifierName)
            {
                IdentifierNameVisitor.Visit((IdentifierName) element.InnerElement, textWriter, providers, true);
            }
            else if (element.InnerElement is GenericName)
            {
                GenericNameVisitor.Visit((GenericName) element.InnerElement, textWriter, providers, true);
            }
            else
            {
                VisitorList.Visit(element.InnerElement);
            }
        }

        public void Visit(TrueLiteralExpression element, IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.Write("true");
        }

        public void Visit(NullLiteralExpression element, IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.Write("nil");
        }

        public void Visit(SimpleAssignmentExpression element, IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.Write(" = ");
        }

        public void Visit(ThisExpression element, IndentedTextWriter textWriter, IProviders providers)
        {
            VisitorList.Visit(element.InnerElement);
        }

        public void Visit(AddExpression element, IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.Write(" +CsLuaMeta.add+ ");
        }

        public void Visit(CharacterLiteralExpression element, IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.Write(element.Text);
        }

        public void Visit(NotEqualsExpression element, IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.Write(" ~= ");
        }

        public void Visit(ReturnStatement element, IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.Write("return ");
        }

        public void Visit(ParenthesizedExpression element, IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.Write("(");
            VisitorList.Visit(element.Statement);
            textWriter.Write(")");
        }

        public void Visit(EqualsExpression element, IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.Write(" == ");
        }

        public void Visit(BaseExpression element, IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.Write("baseElement");
        }

        public void Visit(CastExpression element, IndentedTextWriter textWriter, IProviders providers)
        {
        }

        public void Visit(LogicalNotExpression element, IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.Write("CsLuaMeta._not+");
        }
    }
}