namespace CsLuaConverter.CodeElementAnalysis.Statements
{
    using Expressions;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    public class LocalDeclarationStatement : BaseStatement
    {
        public BaseElement Type;
        public VariableDeclarator Name;
        public ExpressionBase Value;

        public override SyntaxToken Analyze(SyntaxToken token)
        {
            this.Type = GenerateMatchingElement(token);
            token = this.Type.Analyze(token);
            token = token.GetNextToken();

            this.Name = new VariableDeclarator();
            token = this.Name.Analyze(token);
            token = token.GetNextToken();

            ExpectKind(SyntaxKind.EqualsValueClause, token.Parent.GetKind());
            ExpectKind(SyntaxKind.EqualsToken, token.GetKind());
            token = token.GetNextToken();

            this.Value = ExpressionBase.CreateExpression(token);
            token = this.Value.Analyze(token);

            return token;
        }
    }
}