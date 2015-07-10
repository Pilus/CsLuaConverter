namespace CsLuaConverter.SyntaxAnalysis
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.Linq;
    using CsLuaConverter.Providers;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    internal class Switch : ILuaElement
    {
        private readonly List<SwitchCase> switchCases;
        private ILuaElement switchVariable;

        public Switch()
        {
            this.switchCases = new List<SwitchCase>();
        }

        public void WriteLua(IndentedTextWriter textWriter, IProviders providers)
        {
            foreach (SwitchCase switchCase in this.switchCases)
            {
                bool isDefault = !switchCase.Cases.Any();
                if (switchCase.Equals(this.switchCases.First()))
                {
                    if (isDefault)
                    {
                        textWriter.WriteLine("if (true) then");
                    }
                    else
                    {
                        textWriter.Write("if (");
                    }
                }
                else
                {
                    if (isDefault)
                    {
                        textWriter.WriteLine("else");
                    }
                    else
                    {
                        textWriter.WriteLine("elseif (");
                    }
                }

                for (int i = 0; i < switchCase.Cases.Count; i++)
                {
                    this.switchVariable.WriteLua(textWriter, providers);
                    textWriter.Write(" == ");
                    switchCase.Cases[i].WriteLua(textWriter, providers);
                    if (i < switchCase.Cases.Count - 1)
                    {
                        textWriter.Write(" or ");
                    }
                }

                if (!isDefault)
                {
                    textWriter.WriteLine(") then");
                }

                textWriter.Indent++;
                switchCase.Code.WriteLua(textWriter, providers);
                textWriter.Indent--;

                if (switchCase.Equals(this.switchCases.Last()))
                {
                    textWriter.WriteLine("end");
                }
            }
        }

        public SyntaxToken Analyze(SyntaxToken token)
        {
            LuaElementHelper.CheckType(typeof(SwitchStatementSyntax), token.Parent);
            token = token.GetNextToken(); // switch
            token = token.GetNextToken(); // (

            this.switchVariable = new MainCode(t => t.Parent is SwitchStatementSyntax && t.Text.Equals(")"));
            token = this.switchVariable.Analyze(token);

            token = token.GetNextToken(); // )
            token = token.GetNextToken(); // {

            LuaElementHelper.CheckType(typeof(SwitchLabelSyntax), token.Parent);

            var cases = new List<ILuaElement>();
            while (token.Parent is SwitchLabelSyntax)
            {
                if (token.Text.Equals("case"))
                {
                    token = token.GetNextToken();
                    ILuaElement caseVariableName;
                    if (token.Parent is LiteralExpressionSyntax)
                    {
                        caseVariableName = new PredeterminedElement(token.Text);
                    }
                    else
                    {
                        caseVariableName = new VariableName(true);
                    }
                    
                    token = caseVariableName.Analyze(token);
                    token = token.GetNextToken();
                    LuaElementHelper.CheckType(typeof(SwitchLabelSyntax), token.Parent);
                    token = token.GetNextToken(); // :
                    cases.Add(caseVariableName);
                }
                else if (token.Text.Equals("default"))
                {
                    token = token.GetNextToken(); // default
                    token = token.GetNextToken(); // : 
                    cases = new List<ILuaElement>();
                }
                else
                {
                    throw new Exception("Unexpected syntax.");
                }

                if (!(token.Parent is SwitchLabelSyntax))
                {
                    var code = new MainCode(t => t.Parent is SwitchLabelSyntax || t.Parent is SwitchStatementSyntax);
                    token = code.Analyze(token);

                    List<ILuaElement> lastTwoElements =
                        code.Elements.Skip(Math.Max(0, code.Elements.Count() - 2)).Take(2).ToList();
                    if (lastTwoElements.Count() == 2)
                    {
                        if (lastTwoElements[0] is PredeterminedElement && lastTwoElements[1] is PredeterminedElement)
                        {
                            if ((lastTwoElements[0] as PredeterminedElement).Text == "break"
                                && (lastTwoElements[1] as PredeterminedElement).Text == ";")
                            {
                                code.Elements.RemoveRange(code.Elements.Count() - 2, 2);
                            }
                        }
                    }

                    this.switchCases.Add(new SwitchCase
                    {
                        Cases = cases,
                        Code = code
                    });

                    cases = new List<ILuaElement>();
                }
            }

            return token;
        }
    }

    internal struct SwitchCase
    {
        public IList<ILuaElement> Cases;
        public MainCode Code;
    }
}