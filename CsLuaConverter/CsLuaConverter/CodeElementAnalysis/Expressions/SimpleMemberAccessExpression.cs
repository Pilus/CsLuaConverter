namespace CsLuaConverter.CodeElementAnalysis.Expressions
{
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    public class SimpleMemberAccessExpression : ExpressionBase
    {
        public ExpressionBase Member;
        public BaseElement Index;

        public SimpleMemberAccessExpression(SyntaxNode node) : base(node)
        {
        }

        protected override SyntaxToken InnerAnalyze(SyntaxToken token)
        {
            this.Member = this.CreateExpressionFromChild(token);
            token = this.Member.Analyze(token);
            token = token.GetNextToken();

            ExpectKind(SyntaxKind.DotToken, token.GetKind());
            ExpectTrue(token.Parent == this.Node, "Parent of token should be the current node.");
            token = token.GetNextToken();

            this.Index = this.CreateExpressionFromChild(token) ?? GenerateMatchingElement(token);
            token = this.Index.Analyze(token);

            return token;
        }
    }
}