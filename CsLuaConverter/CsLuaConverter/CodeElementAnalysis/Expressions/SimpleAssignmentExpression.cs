namespace CsLuaConverter.CodeElementAnalysis.Expressions
{
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    public class SimpleAssignmentExpression : ExpressionBase
    {
        public ExpressionBase Target;
        public BaseElement Value;
        public SimpleAssignmentExpression(SyntaxNode node) : base(node)
        {
        }

        protected override SyntaxToken InnerAnalyze(SyntaxToken token)
        {
            this.Target = this.CreateExpressionFromChild(token);
            token = this.Target.Analyze(token);
            token = token.GetNextToken();

            ExpectKind(SyntaxKind.EqualsToken, token.GetKind());
            ExpectTrue(token.Parent == this.Node, "Parent of token should be the current node.");
            token = token.GetNextToken();

            this.Value = this.CreateExpressionFromChild(token);
            token = this.Value.Analyze(token);

            return token;
        }
    }
}