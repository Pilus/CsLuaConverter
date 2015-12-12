namespace CsLuaConverter.CodeElementAnalysis.Statements
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    public class Statement : ContainerElement
    {
        public StatementInfo StatementInfo;

        public string EndToken;

        public override SyntaxToken Analyze(SyntaxToken token)
        {
            token = base.Analyze(token);
            this.EndToken = token.Text;
            this.StatementInfo = DetermineStatementInfo(this.ContainedElements);
            return token;
        }

        public override bool IsTokenAcceptedInContainer(SyntaxToken token)
        {
            return !token.Parent.IsKind(SyntaxKind.Block);
        }

        public override bool ShouldContainerBreak(SyntaxToken token)
        {
            return 
                token.IsKind(SyntaxKind.CommaToken) || 
                token.IsKind(SyntaxKind.CloseBraceToken) ||
                token.IsKind(SyntaxKind.CloseParenToken) ||
                token.IsKind(SyntaxKind.SemicolonToken);
        }

        private static readonly IDictionary<Type, Func<IList<BaseElement>, int, StatementInfo>> InfoProviders = new Dictionary<Type, Func<IList<BaseElement>, int, StatementInfo>>()
        {
            {typeof(VariableDeclarator), (elements, i) =>
            {
                if (i == 1 && (elements[0] is IdentifierName || elements[0] is GenericName))
                {
                    return new StatementInfo(StatementType.VariableDeclaration, i);
                }

                return null;
            }},
            /*
            {typeof(SimpleAssignmentExpression), (elements, i) => new StatementInfo(StatementType.SimpleAssignment, i)},
            {typeof(EqualsValueClause), (elements, i) => new StatementInfo(StatementType.SimpleAssignment, i)},
            {typeof(ArgumentList), (elements, i) => new StatementInfo(StatementType.MethodCall, i)},
            {typeof(IsExpression), (elements, i) => new StatementInfo(StatementType.IsExpression, i)},
            {typeof(AsExpression), (elements, i) => new StatementInfo(StatementType.AsExpression, i)},
            {typeof(PostIncrementExpression), (elements, i) => new StatementInfo(StatementType.PlusPlus, i)},
            {typeof(EqualsExpression), (elements, i) => new StatementInfo(StatementType.EqualsExpression, i)}, */
        };

        private static StatementInfo DetermineStatementInfo(IList<BaseElement> elements)
        {
            var statementTypes = elements
                .Where(e => InfoProviders.ContainsKey(e.GetType()))
                .Select(e => InfoProviders[e.GetType()](elements, elements.IndexOf(e)))
                .ToList();

            if (!statementTypes.Any())
            {
                return new StatementInfo(StatementType.SimpleStatement, null);
            }

            if (statementTypes.Count() > 1)
            {
                throw new StatementException("Multiple elements with providers found.");
            }

            return statementTypes.Single();
        }
    }
}