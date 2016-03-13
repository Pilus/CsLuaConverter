namespace CsLuaConverter.CodeElementAnalysis.Expressions
{
    using Microsoft.CodeAnalysis;

    public class ElementAccessExpression : ExpressionBase
    {
        public ExpressionBase Element;
        public BracketedArgumentList ArgumentList;

        public ElementAccessExpression(SyntaxNode node) : base(node)
        {
        }

        protected override SyntaxToken InnerAnalyze(SyntaxToken token)
        {
            this.Element = this.CreateExpressionFromChild(token);
            token = this.Element.Analyze(token);
            token = token.GetNextToken();

            this.ArgumentList = new BracketedArgumentList();
            token = this.ArgumentList.Analyze(token);

            throw new System.NotImplementedException();
        }
    }
}