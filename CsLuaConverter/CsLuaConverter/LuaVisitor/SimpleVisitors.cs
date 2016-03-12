namespace CsLuaConverter.LuaVisitor
{
    using System.CodeDom.Compiler;
    using CodeElementAnalysis;
    using CodeElementAnalysis.Statements;
    using Providers;

    public class SimpleVisitors : 
        IVisitor<FalseLiteralExpression>, IVisitor<EqualsValueClause>, IVisitor<TrueLiteralExpression>,
        IVisitor<NullLiteralExpression>, IVisitor<AddExpression>,
        IVisitor<CharacterLiteralExpression>, IVisitor<NotEqualsExpression>, IVisitor<ReturnStatement>, 
        IVisitor<EqualsExpression>, IVisitor<CastExpression>, IVisitor<LogicalNotExpression>, IVisitor<SubtractExpression>,
        IVisitor<MultiplyExpression>, IVisitor<UnaryMinusExpression>, IVisitor<GreaterThanExpression>, IVisitor<LessThanExpression>,
        IVisitor<LogicalAndExpression>, IVisitor<DivideExpression>, IVisitor<BreakStatement>, IVisitor<LessThanOrEqualExpression>,
        IVisitor<GreaterThanOrEqualExpression>
    {
        public void Visit(FalseLiteralExpression element, IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.Write("false");
        }

        public void Visit(EqualsValueClause element, IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.Write(" = ");
        }

        public void Visit(TrueLiteralExpression element, IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.Write("true");
        }

        public void Visit(NullLiteralExpression element, IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.Write("nil");
        }

        public void Visit(AddExpression element, IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.Write(" +_M.Add+ ");
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

        public void Visit(EqualsExpression element, IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.Write(" == ");
        }

        public void Visit(CastExpression element, IndentedTextWriter textWriter, IProviders providers)
        {
        }

        public void Visit(LogicalNotExpression element, IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.Write("_M.NOT+");
        }

        public void Visit(SubtractExpression element, IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.Write(" - ");
        }

        public void Visit(MultiplyExpression element, IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.Write(" * ");
        }

        public void Visit(UnaryMinusExpression element, IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.Write(" - ");
        }

        public void Visit(GreaterThanExpression element, IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.Write(" > ");
        }

        public void Visit(LessThanExpression element, IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.Write(" < ");
        }

        public void Visit(LogicalAndExpression element, IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.Write(" and ");
        }

        public void Visit(DivideExpression element, IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.Write("/");
        }

        public void Visit(BreakStatement element, IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.Write("break");
        }

        public void Visit(LessThanOrEqualExpression element, IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.Write(" <= ");
        }

        public void Visit(GreaterThanOrEqualExpression element, IndentedTextWriter textWriter, IProviders providers)
        {
            textWriter.Write(" >= ");
        }
    }
}