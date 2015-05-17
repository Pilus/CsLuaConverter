namespace CsToLua.SyntaxAnalysis
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.Linq;
    using CsLuaCompiler.SyntaxAnalysis.NameAndTypeProvider;
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
                    return new PredeterminedElement("__not+");
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
            {typeof(GenericNameSyntax), token => new VariableType(true, true)},
            {typeof(ParenthesizedExpressionSyntax), token => new PredeterminedElement(token.Text)},
            {
                typeof(LiteralExpressionSyntax),
                token =>
                    token.Text.Equals("null") ? new PredeterminedElement("nil") : new PredeterminedElement(token.Text)
            },
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
            {typeof(BaseExpressionSyntax), token => new PredeterminedElement("class.__base")},
            {typeof(EmptyStatementSyntax), token => new PredeterminedElement("")},
            {typeof(TypeOfExpressionSyntax), token =>
            {
                if (token.Text == "(" || token.Text == ")")
                {
                    return new PredeterminedElement("'");
                }
                return new PredeterminedElement("");
            }},
        };

        public MainCode(Func<SyntaxToken, bool> breakCondition)
        {
            this.breakCondition = breakCondition;
        }

        public void WriteLua(IndentedTextWriter textWriter, INameAndTypeProvider nameProvider)
        {
            this.Elements.ForEach(element => element.WriteLua(textWriter, nameProvider));
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
                }

                if ((element is ArgumentList || element is Initializer) && this.Elements.Count() >= 2)
                {
                    var twoLast = this.Elements.Skip(this.Elements.Count - 2);
                    if (twoLast.First() is New && (twoLast.Last() is VariableType || twoLast.Last() is VariableName))
                    {
                        if (twoLast.Last() is VariableType)
                        {
                            var variableType = this.Elements.Last() as VariableType;
                            var genericsString = variableType.GetQuotedGenericTypeString();
                            if (genericsString == null)
                            {
                                genericsString = "";
                            }

                            this.Elements.Add(new PredeterminedElement(string.Format("({0}).__Cstor", genericsString)));
                        }
                        else
                        {
                            this.Elements.Add(new PredeterminedElement("().__Cstor"));
                        }                        
                    }
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