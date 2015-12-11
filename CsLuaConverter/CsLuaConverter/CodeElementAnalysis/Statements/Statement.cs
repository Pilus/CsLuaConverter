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

        public override SyntaxToken Analyze(SyntaxToken token)
        {
            token = base.Analyze(token);
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
            var elementsWithProviders = elements.Where(e => InfoProviders.ContainsKey(e.GetType())).ToList();

            if (!elementsWithProviders.Any())
            {
                return new StatementInfo(StatementType.ResultingStatement, null);
            }

            if (elementsWithProviders.Count() > 1)
            {
                throw new StatementException("Multiple elements with providers found.");
            }

            var provider = elementsWithProviders.Single();
            return InfoProviders[provider.GetType()](elements, elements.IndexOf(provider));
        }
    }
}