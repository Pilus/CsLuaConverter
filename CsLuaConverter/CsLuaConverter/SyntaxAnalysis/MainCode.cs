﻿namespace CsLuaConverter.SyntaxAnalysis
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.Linq;
    using CsLuaConverter.Providers;
    using CsLuaConverter.Providers.TypeProvider;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    internal class MainCode : ILuaElement
    {
        public readonly List<ILuaElement> Elements = new List<ILuaElement>();
        private readonly Func<SyntaxToken, bool> breakCondition;

        private readonly Dictionary<Type, Func<SyntaxToken, ILuaElement>> typeMapping = new Dictionary
            <Type, Func<SyntaxToken, ILuaElement>>
        {
            {typeof(IfStatementSyntax), token => new IfStatement()},
            {typeof(ForEachStatementSyntax), token => new ForeachLoop()},
            {typeof(WhileStatementSyntax), token => new WhileLoop()},
            {typeof(ForStatementSyntax), token => new ForLoop()},
            {
                typeof(ReturnStatementSyntax), token =>
                {
                    if (token.Text.Equals("return"))
                    {
                        return new PredeterminedElement("return ");
                    }
                    return new PredeterminedElement(";", null, true);
                }
            },
            {typeof(ThisExpressionSyntax), token => new This()},
            {typeof(BinaryExpressionSyntax), token => new BinaryExpression()},
            {typeof(PrefixUnaryExpressionSyntax), token =>
                {
                    if (token.Text.Equals("-"))
                    {
                        return new PredeterminedElement(token.Text);
                    }
                    return new PredeterminedElement("CsLuaMeta._not+");
                }
            },
            {typeof(ElseClauseSyntax), token => new Else()},
            {
                typeof(LocalDeclarationStatementSyntax), token =>
                {
                    if (token.Text.Equals("const"))
                    {
                        return new PredeterminedElement("");
                    }
                    return new PredeterminedElement(";", null, true);
                }
            },
            {
                typeof(IdentifierNameSyntax), token =>
                {
                    if (token.Text.Equals("var"))
                    {
                        return new Var();
                    }
                    return new VariableName(true);
                }
            },
            {typeof(EqualsValueClauseSyntax), token => new PredeterminedElement(" = ")},
            {typeof(ExpressionStatementSyntax), token => new PredeterminedElement(";", null, true)},
            {typeof(ObjectCreationExpressionSyntax), token => new New()},
            {typeof(MemberAccessExpressionSyntax), token => new ReferencedVariableName()},
            {typeof(ArrayCreationExpressionSyntax), token => new ArrayCreation()},
            {typeof(SimpleLambdaExpressionSyntax), token => new LambdaExpression()},
            {
                typeof(ArgumentListSyntax), token =>
                {
                    if (token.Text != "(") throw new Exception("Closing ArgumentListSyntax unexpected.");
                    return new ArgumentList();
                }
            },
            {
                typeof(BracketedArgumentListSyntax), token =>
                {
                    if (token.Text != "[") throw new Exception("Closing BracketedArgumentListSyntax unexpected.");
                    return new BracketedArgumentList();
                }
            },
            {typeof(PredefinedTypeSyntax), token => new PredefinedType()},
            {typeof(GenericNameSyntax), token => new VariableDefinition(true, true)},
            {typeof(ParenthesizedExpressionSyntax), token => new PredeterminedElement(token.Text)},
            {typeof(LiteralExpressionSyntax), token => new LiteralExpression()},
            {typeof(VariableDeclaratorSyntax), token => new PredeterminedElement(token.Text)},
            {typeof(TypeArgumentListSyntax), token => new PredeterminedElement("")},
            {typeof(ThrowStatementSyntax), token => new Throw()},
            {typeof(ImplicitArrayCreationExpressionSyntax), token => new PredeterminedElement("")},
            {
                typeof(InitializerExpressionSyntax),
                token =>
                    new Initializer(token.GetPreviousToken().Parent is ArgumentListSyntax,
                        token.GetPreviousToken().Parent is IdentifierNameSyntax)
            },
            {
                typeof(ConditionalExpressionSyntax), token =>
                {
                    if (token.Text.Equals("?"))
                        return new PredeterminedElement(" and ");
                    if (token.Text.Equals(":"))
                        return new PredeterminedElement(" or ");
                    throw new Exception("Unknown conditional expression.");
                }
            },
            {typeof(ParameterSyntax), token => new PredeterminedElement(token.Text)},
            {typeof(CastExpressionSyntax), token => new Cast()},
            {typeof(AnonymousMethodExpressionSyntax), token => new Delegate()},
            {typeof(ParameterListSyntax), token => new ParameterList()},
            {typeof(ParenthesizedLambdaExpressionSyntax), token => new ParenthesizedLambdaExpression()},
            {typeof(SwitchStatementSyntax), token => new Switch()},
            {typeof(BreakStatementSyntax), token => new PredeterminedElement(token.Text)},
            {typeof(PostfixUnaryExpressionSyntax), token => new PlusPlus()},
            {typeof(NullableTypeSyntax), token => null},
            {typeof(BaseExpressionSyntax), token => new PredeterminedElement("baseElement")},
            {typeof(EmptyStatementSyntax), token => new PredeterminedElement("")},
            {typeof(TypeOfExpressionSyntax), token =>
            {
                if (token.Text == "(")
                {
                    return new PredeterminedElement("System.Type(\"");
                }
                else if (token.Text == ")")
                {
                    return new PredeterminedElement("\")");
                }
                return new PredeterminedElement("");
            }},
            {typeof(TryStatementSyntax), token => new Try()}
        };

        public MainCode(Func<SyntaxToken, bool> breakCondition)
        {
            this.breakCondition = breakCondition;
        }

        public void WriteLua(IndentedTextWriter textWriter, IProviders providers)
        {
            this.Elements.ForEach(element => element.WriteLua(textWriter, providers));
        }

        public SyntaxToken Analyze(SyntaxToken token)
        {
            while (!this.breakCondition(token))
            {
                Func<SyntaxToken, ILuaElement> action = null;
                Type t = token.Parent.GetType();
                if (this.typeMapping.ContainsKey(t))
                {
                    action = this.typeMapping[t];
                }
                else
                {
                    //action = text => new PredeterminedElement("*!*" + token.Parent.GetType().Name + "*@*" + text + "*#*");
                    throw new Exception("Unhandled element");
                }

                ILuaElement element = action(token);

                // Special situations: Variable declaration should remove the type before it.
                if (token.Parent is VariableDeclaratorSyntax &&
                    (this.Elements.Last() is VariableName || this.Elements.Last() is PredefinedType))
                {
                    this.Elements.Remove(this.Elements.Last());
                    this.Elements.Add(new PredeterminedElement("local ",
                        new ScopeElement(token.Text) {IsFromClass = false}));
                }

                // Should remove the ( before the lambda expression.
                if (token.Parent is SimpleLambdaExpressionSyntax)
                {
                    this.Elements.Remove(this.Elements.Last());
                }

                if (element is ParenthesizedLambdaExpression)
                {
                    (element as ParenthesizedLambdaExpression).ParameterList = this.Elements.Last() as ParameterList;
                    this.Elements.Remove(this.Elements.Last());
                }

                // If a ++, notify about previous element
                if (element is PlusPlus)
                {
                    (element as PlusPlus).PreviousElement = this.Elements.Last();
                }

                if (element is BinaryExpression)
                {
                    (element as BinaryExpression).PreviousElement = this.Elements.Last();
                    if (token.Text.Equals("is"))
                    {
                        this.Elements.Remove(this.Elements.Last());
                    }
                }

                var last = this.Elements.LastOrDefault();

                if (element is ReferencedVariableName)
                {
                    var secondLast = this.Elements.Skip(this.Elements.Count - 2).First();

                    if (last is PredeterminedElement && (last as PredeterminedElement).Text.Equals(")"))
                    {
                        last = secondLast;
                    }

                    if (last is LiteralExpression)
                    {
                        this.Elements.Insert(this.Elements.IndexOf(last), new PredeterminedElement("("));
                        this.Elements.Add(new PredeterminedElement("%_M.DOT)"));
                    }
                }

                if (element is Initializer && this.Elements.Count > 1)
                {
                    var secondLast = this.Elements.Skip(this.Elements.Count - 2).First();
                    if (secondLast is New)
                    {
                        this.Elements.Insert(this.Elements.Count - 2, new PredeterminedElement("("));
                    }
                }

                
                if (element is BracketedArgumentList && (last is VariableName || last is This))
                {
                    this.Elements.Insert(this.Elements.Count - 1, new PredeterminedElement("("));
                }

                if (element != null)
                {
                    token = element.Analyze(token);

                    this.Elements.Add(element);
                }

                token = token.GetNextToken();
            }

            ILuaElement first = this.Elements.FirstOrDefault();
            if (first is PredeterminedElement && ((PredeterminedElement) first).Text == "not(")
            {
                this.Elements.Add(new PredeterminedElement(")"));
            }

            return token;
        }
    }
}