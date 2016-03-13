namespace CsLuaConverter.CodeElementAnalysis.Expressions
{
    using System;
    using System.CodeDom;
    using System.Diagnostics;
    using System.Linq;
    using System.Net;
    using System.Reflection;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    public abstract class ExpressionBase : BaseElement
    {
        protected readonly SyntaxNode Node;

        protected ExpressionBase(SyntaxNode node)
        {
            this.Node = node;
        }


        [DebuggerNonUserCode]
        public override SyntaxToken Analyze(SyntaxToken firstToken)
        {
            this.ExpectFirstToken(firstToken);
            var lastToken = this.InnerAnalyze(firstToken);
            this.ExpectLastToken(lastToken);
            return lastToken;
        }

        protected abstract SyntaxToken InnerAnalyze(SyntaxToken firstToken);


        [DebuggerNonUserCode]
        public void ExpectLastToken(SyntaxToken token)
        {
            var expected = this.Node.GetLastToken();
            if (expected != token)
            {
                throw new Exception($"Expected {expected.GetKind()} as the current token, being last token in the expression of ({this.Node.GetKind()}): {this.Node}. Got  ({token.GetKind()}).");
            }
        }

        [DebuggerNonUserCode]
        public void ExpectFirstToken(SyntaxToken token)
        {
            var expected = this.Node.GetFirstToken();
            if (expected != token)
            {
                throw new Exception($"Expected {expected.GetKind()} as the current token, being first token in the expression of ({this.Node.GetKind()}): {this.Node}. Got  ({token.GetKind()}).");
            }
        }

        protected ExpressionBase CreateExpressionFromChild(SyntaxToken token)
        {
            return CreateExpression(token.GetChildOfAnchestor(this.Node));
        }

        public static ExpressionBase CreateExpression(SyntaxToken token)
        {
            var kind = token.Parent.GetKind();
            var parent = token.Parent;
            var node = token.Parent;

            while (parent != null)
            {
                if (parent.GetKind().ToString().Contains("Expression") && parent.GetKind() != SyntaxKind.ExpressionStatement)
                {
                    kind = parent.GetKind();
                    node = parent;
                }
                
                parent = parent.Parent;
            }

            return CreateExpression(node);
        }

        public static ExpressionBase CreateExpression(SyntaxNode node)
        {
            var kind = node.GetKind();
            var expression = TryCreateExpression(node);
            if (expression == null && kind.ToString().Contains("Expression"))
            {
                throw new Exception($"Unsupported expression {kind}");
            }

            return expression;
        }

        private static ExpressionBase TryCreateExpression(SyntaxNode node)
        {
            var kind = node.GetKind();
            var type = Assembly.GetExecutingAssembly().GetTypes().SingleOrDefault(t => t.Name.Equals(kind.ToString()) && t.IsSubclassOf(typeof(ExpressionBase)));
            return type?.GetConstructors().Single().Invoke(new object[] {node}) as ExpressionBase;
        }
    }
}