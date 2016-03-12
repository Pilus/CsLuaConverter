namespace CsLuaConverter.CodeElementAnalysis.Statements
{
    using System.Collections.Generic;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    public class SwitchStatement : BaseStatement
    {
        public BaseElement SwitchTarget;
        public List<SwitchElement> SwitchElements = new List<SwitchElement>();

        public override SyntaxToken Analyze(SyntaxToken token)
        {
            ExpectKind(SyntaxKind.SwitchStatement, token.Parent.GetKind());
            ExpectKind(SyntaxKind.SwitchKeyword, token.GetKind());
            token = token.GetNextToken();

            ExpectKind(SyntaxKind.SwitchStatement, token.Parent.GetKind());
            ExpectKind(SyntaxKind.OpenParenToken, token.GetKind());
            token = token.GetNextToken();

            this.SwitchTarget = GenerateMatchingElement(token);
            token = this.SwitchTarget.Analyze(token);
            token = token.GetNextToken();

            ExpectKind(SyntaxKind.SwitchStatement, token.Parent.GetKind());
            ExpectKind(SyntaxKind.CloseParenToken, token.GetKind());
            token = token.GetNextToken();

            ExpectKind(SyntaxKind.SwitchStatement, token.Parent.GetKind());
            ExpectKind(SyntaxKind.OpenBraceToken, token.GetKind());
            token = token.GetNextToken();

            while (!(token.Parent.IsKind(SyntaxKind.SwitchStatement) && token.IsKind(SyntaxKind.CloseBraceToken)))
            {
                var element = new SwitchElement();
                element.Statements = new List<BaseElement>();

                ExpectKind(new[] {SyntaxKind.CaseSwitchLabel, SyntaxKind.DefaultSwitchLabel}, token.Parent.GetKind());
                ExpectKind(new[] {SyntaxKind.CaseKeyword, SyntaxKind.DefaultKeyword}, token.GetKind());
                
                if (token.IsKind(SyntaxKind.DefaultKeyword))
                {
                    element.Default = true;
                }
                else
                {
                    token = token.GetNextToken();
                    element.Identifier = GenerateMatchingElement(token);
                    token = element.Identifier.Analyze(token);
                }

                token = token.GetNextToken();

                ExpectKind(new[] { SyntaxKind.CaseSwitchLabel, SyntaxKind.DefaultSwitchLabel }, token.Parent.GetKind());
                ExpectKind(SyntaxKind.ColonToken, token.GetKind());
                token = token.GetNextToken();

                while (!(token.Parent.IsKind(SyntaxKind.CaseSwitchLabel) 
                      || token.Parent.IsKind(SyntaxKind.SwitchStatement) 
                      || token.Parent.IsKind(SyntaxKind.DefaultSwitchLabel)))
                {
                    var statement = CreateStatement(token);

                    token = statement.Analyze(token);
                    element.Statements.Add(statement);
                    token = token.GetNextToken();
                }

                this.SwitchElements.Add(element);
            }

            ExpectKind(SyntaxKind.SwitchStatement, token.Parent.GetKind());
            ExpectKind(SyntaxKind.CloseBraceToken, token.GetKind());
            return token;
        }
    }

    public struct SwitchElement
    {
        public BaseElement Identifier;
        public List<BaseElement> Statements;
        public bool Default;
    }
}